using Microsoft.Extensions.Logging;

namespace Meteostanice.Services
{
    public class MeteoDownloader
    {

        private readonly HttpClient _httpClient;
        private readonly ILogger<MeteoDownloader> _logger;
        public MeteoDownloader(HttpClient httpClient, ILogger<MeteoDownloader> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> DownloadXmlAsync(string url)
        {
            try
            {

                return await _httpClient.GetStringAsync(url);

            }

            catch (Exception ex)
            {

                _logger.LogError(ex, $"Nastala chyba při stahování XML souboru z adresy {url}: {ex.Message}");
                throw;

            }
        }
    }
}
