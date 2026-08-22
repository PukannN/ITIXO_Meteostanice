using Meteostanice.Data;
using Meteostanice.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteostanice.Services
{
    public class MeteoRepository
    {
        private MeteoDbContext _context;

        public MeteoRepository(MeteoDbContext context)
        {
            _context = context;
        }

        public async Task SaveMeteoRecordAsync(MeteoRecord record)
        {
            try
            {
                _context.MeteoRecords.Add(record);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving MeteoRecord: {ex.Message}");
                throw;
            }
        }
    }
}
