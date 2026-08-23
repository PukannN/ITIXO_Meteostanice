using Newtonsoft.Json;
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
                return JsonConvert.SerializeXNode(xmlDoc, Formatting.Indented);

            }
            catch (Exception ex)
            {
                throw new Exception($"Chyba při konverzi XML na JSON: {ex.Message}", ex);
            }
        }
    }
}
