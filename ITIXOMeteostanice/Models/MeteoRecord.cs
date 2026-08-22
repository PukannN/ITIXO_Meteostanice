using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteostanice.Models
{
    public class MeteoRecord
    {
        public int Id { get; set; }
        public DateTime DownloadedAt { get; set; }
        public bool IsSuccessful { get; set; }
        public string? ErrorMessage { get; set; }
        public string? JSONData { get; set; }

    }
}
