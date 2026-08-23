using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Meteostanice.Services
{
    public class XMLtoJsonConverter
    {

        public static string Convert(string xmlString)
        {
            try
            {
                var xmlDoc = XDocument.Parse(xmlString);
                return JsonConvert.SerializeXNode(xmlDoc, Formatting.Indented);
                
            }
            catch (Exception ex)
            {
                throw new Exception($"Chyba při konverzi XML na JSON: {ex.Message}", ex);
            }
        }
    }
}
