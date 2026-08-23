using Meteostanice.Models;
using Microsoft.EntityFrameworkCore;

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
