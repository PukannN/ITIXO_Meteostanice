using Meteostanice.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Meteostanice.Data
{
    public class MeteoDbContext : DbContext
    {
        public DbSet<MeteoRecord> MeteoRecords { get; set; }

        public MeteoDbContext(DbContextOptions<MeteoDbContext> options) : base(options)
        {
        }
    }
}
