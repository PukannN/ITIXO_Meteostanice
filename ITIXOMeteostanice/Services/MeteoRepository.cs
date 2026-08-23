using Meteostanice.Data;
using Meteostanice.Models;

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
                throw new Exception($"Chyba při ukládání záznamu do databáze: {ex.Message}", ex);
            }
        }
    }
}
