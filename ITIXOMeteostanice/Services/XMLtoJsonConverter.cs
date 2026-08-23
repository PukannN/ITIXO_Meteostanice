using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Meteostanice.Services
{
    public class XMLtoJsonConverter
    {

        public static string Convert(string xmlString)
        {
            try
            {
                var xmlDoc = XDocument.Parse(xmlString);
                string jsonText = Newtonsoft.Json.JsonConvert.SerializeXNode(xmlDoc, Newtonsoft.Json.Formatting.Indented);
                return jsonText;
            }
            catch (Exception ex)
            {
                throw new Exception($"Chyba při konverze XML na JSON: {ex.Message}", ex);
            }
        }
    }
}
