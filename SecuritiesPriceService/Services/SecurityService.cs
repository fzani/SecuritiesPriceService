using BNP.SecuritiesPriceService.Models;
using BNP.SecuritiesPriceService.Repositories;

namespace BNP.SecuritiesPriceService.Services;


public class SecurityService : ISecurityService
{
    private readonly ISecurityRepository _repository;
    private readonly HttpClient _httpClient;

    public SecurityService(ISecurityRepository repository, HttpClient httpClient)
    {
        _repository = repository;
        _httpClient = httpClient;
    }

    public async Task RetrieveAndStorePricesAsync(IEnumerable<string> isins)
    {
        foreach (var isin in isins)
        {
            var price = await GetPriceFromApiAsync(isin);
            var security = new Security { ISIN = isin, Price = price };
            await _repository.SaveSecurityAsync(security);
        }
    }

    private async Task<decimal> GetPriceFromApiAsync(string isin)
    {
        var response = await _httpClient.GetAsync($"https://securities.dataprovider.com/securityprice/{isin}");
        response.EnsureSuccessStatusCode();
        var price = await response.Content.ReadAsStringAsync();
        return decimal.Parse(price);
    }
}