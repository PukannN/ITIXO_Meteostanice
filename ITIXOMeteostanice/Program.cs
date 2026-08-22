using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Meteostanice.Services;
using Meteostanice.Data;
using Meteostanice.Models;
using Microsoft.SqlServer.Server;
using System.Xml.Linq;



namespace Meteostanice
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            var downloader = new MeteoDownloader(new HttpClient());

            using (var context = new MeteoDbContext())
            {
                
                await context.Database.EnsureCreatedAsync();

                var repository = new MeteoRepository(context);

                try
                {
                    // Používám /raw/ path segment, abych získat čistý XML 
                    string body = await downloader.DownloadXmlAsync("https://pastebin.com/raw/PMQ23ueqDV");

                    string jsonString = XMLtoJsonConverter.Convert(body);

                    var record = new MeteoRecord
                    {
                        DownloadedAt = DateTime.Now,
                        IsSuccessful = true,
                        ErrorMessage = null,
                        JSONData = jsonString
                    };

                    await repository.SaveMeteoRecordAsync(record);
                    Console.WriteLine("Záznam úspěšně uložen do databáze");
                    //Console.WriteLine(jsonString);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Chyba při zpracování: {ex.Message}");

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
    }
}
