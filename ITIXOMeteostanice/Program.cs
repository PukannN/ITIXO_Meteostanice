using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Meteostanice.Services;
using Microsoft.SqlServer.Server;
using System.Xml.Linq;

namespace Meteostanice
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            MeteoDownloader downloader = new MeteoDownloader(new HttpClient());
            string body = await downloader.DownloadXmlAsync("https://pastebin.com/raw/PMQueqDV");

            string jsonString = XMLtoJsonConverter.Convert(body);

            Console.WriteLine(jsonString);


        }
    }
}
