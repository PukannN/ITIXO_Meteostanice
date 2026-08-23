using ITIXOMeteostanice.Configuration;
using Meteostanice.Data;
using Meteostanice.Models;
using Meteostanice.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Meteostanice;

public class MeteoWorker : BackgroundService
{
    private readonly MeteoDownloader _downloader;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<MeteoWorker> _logger;
    private readonly MeteoOptions _options;

    public MeteoWorker(
        MeteoDownloader downloader,
        IServiceScopeFactory scopeFactory,
        ILogger<MeteoWorker> logger,
        IOptions<MeteoOptions> options)
    {
        _downloader = downloader;
        _scopeFactory = scopeFactory;
        _logger = logger;
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("MeteoWorker spuštěn. Nastavený interval: {Interval} hod.", _options.DownloadInterval);

        var interval = TimeSpan.FromHours(_options.DownloadInterval);
        using var timer = new PeriodicTimer(interval);

        do
        {
            try
            {
                _logger.LogInformation("Začínám zpracování meteo dat...");
                await ProcessMeteoDataAsync(stoppingToken);
                _logger.LogInformation("Zpracování dat bylo úspěšně dokončeno.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Při zpracování meteo dat došlo k chybě!");
            }

        } while (await timer.WaitForNextTickAsync(stoppingToken));
    }

    private async Task ProcessMeteoDataAsync(CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<MeteoDbContext>();
        var repository = new MeteoRepository(context);

        try
        {
            string body = await _downloader.DownloadXmlAsync(_options.DownloadURL);
            string jsonString = XMLtoJsonConverter.Convert(body);

            var record = new MeteoRecord
            {
                DownloadedAt = DateTime.Now,
                IsSuccessful = true,
                ErrorMessage = null,
                JSONData = jsonString
            };

            await repository.SaveMeteoRecordAsync(record);
            _logger.LogInformation("Záznam úspěšně uložen do databáze.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Chyba při zpracování: {Message}", ex.Message);

            var errorRecord = new MeteoRecord
            {
                DownloadedAt = DateTime.Now,
                IsSuccessful = false,
                ErrorMessage = ex.Message,
                JSONData = null
            };

            await repository.SaveMeteoRecordAsync(errorRecord);
        }
    }
}