using ITIXOMeteostanice.Configuration;
using Meteostanice;
using Meteostanice.Data;
using Meteostanice.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string nebyl v appsettings.json nalezen!");
}

// Registrace DbContextu
builder.Services.AddDbContext<MeteoDbContext>(options =>
    options.UseSqlServer(connectionString));

// Registrace nastavení
builder.Services.Configure<MeteoOptions>(builder.Configuration.GetSection("MeteoSettings"));

// Registrace HttpClientu
builder.Services.AddHttpClient<MeteoDownloader>();

// Regisrace Workeru
builder.Services.AddHostedService<MeteoWorker>();

var host = builder.Build();

// Inicializace DB
using (var scope = host.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MeteoDbContext>();
    await dbContext.Database.EnsureCreatedAsync();
}

await host.RunAsync();