using BNP.SecuritiesPriceService.Models;

namespace BNP.SecuritiesPriceService.Repositories;

public interface ISecurityRepository
{
    Task<Security> GetSecurityByISINAsync(string isin);
    Task SaveSecurityAsync(Security security);
}