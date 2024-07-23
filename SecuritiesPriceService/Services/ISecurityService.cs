
namespace BNP.SecuritiesPriceService.Services
{
    public interface ISecurityService
    {
        Task RetrieveAndStorePricesAsync(IEnumerable<string> isins);
    }
}