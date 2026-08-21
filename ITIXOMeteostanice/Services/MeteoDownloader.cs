using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Meteostanice.Services
{
    public class MeteoDownloader
    {

        private readonly HttpClient _httpClient;
        public MeteoDownloader(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> DownloadXmlAsync(string url)
        {
            try
            { 
                //TODO: SQL LOG
            return await _httpClient.GetStringAsync(url);
            }
  
            catch (Exception ex)
            {
                //TODO: SQL LOG
                Console.WriteLine($"Error downloading XML from {url}: {ex.Message}");
                throw;
            }
        }
    }
}
