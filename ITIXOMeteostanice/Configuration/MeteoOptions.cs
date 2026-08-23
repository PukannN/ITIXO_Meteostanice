using System;
using System.Collections.Generic;
using System.Text;

namespace ITIXOMeteostanice.Configuration
{
    public class MeteoOptions
    {
        public string DownloadURL { get; set; } = String.Empty;
        public int DownloadInterval { get; set; } = 1; // v hodinách
    }
}
