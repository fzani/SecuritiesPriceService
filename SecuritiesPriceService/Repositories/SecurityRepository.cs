using BNP.SecuritiesPriceService.Data;
using BNP.SecuritiesPriceService.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace BNP.SecuritiesPriceService.Repositories
{
    public class SecurityRepository : ISecurityRepository
    {
        private readonly SecuritiesDbContext _context;

        public SecurityRepository(SecuritiesDbContext context)
        {
            _context = context;
        }

        public async Task<Security> GetSecurityByISINAsync(string isin)
        {
            return await _context.Securities
                .FirstOrDefaultAsync(s => s.ISIN == isin);
        }

        public async Task SaveSecurityAsync(Security security)
        {
            var existingSecurity = await GetSecurityByISINAsync(security.ISIN);
            if (existingSecurity != null)
            {
                // Update the existing entity
                existingSecurity.Price = security.Price;
                _context.Entry(existingSecurity).State = EntityState.Modified;
            }
            else
            {
                // Add the new entity
                _context.Securities.Add(security);
            }

            await _context.SaveChangesAsync();
        }
    }
}
