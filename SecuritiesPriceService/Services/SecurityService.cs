using BNP.SecuritiesPriceService.Models;
using BNP.SecuritiesPriceService.Repositories;
using System.Globalization;

namespace BNP.SecuritiesPriceService.Services
{
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
            var securities = await RetrievePricesAsync(isins);
            await StoreSecuritiesAsync(securities);
        }

        public async Task<decimal> GetPriceByIsinAsync(string isin)
        {
            return await GetPriceFromApiAsync(isin);
        }

        public async Task SaveSecurityAsync(Security security)
        {
            await _repository.SaveSecurityAsync(security);
        }

        private async Task<IEnumerable<Security>> RetrievePricesAsync(IEnumerable<string> isins)
        {
            var securities = new List<Security>();

            foreach (var isin in isins)
            {
                var price = await GetPriceFromApiAsync(isin);
                var security = new Security { ISIN = isin, Price = price };
                securities.Add(security);
            }

            return securities;
        }

        private async Task StoreSecuritiesAsync(IEnumerable<Security> securities)
        {
            foreach (var security in securities)
            {
                await _repository.SaveSecurityAsync(security);
            }
        }

        private async Task<decimal> GetPriceFromApiAsync(string isin)
        {
            var response = await _httpClient.GetAsync($"https://securities.dataprovider.com/securityprice/{isin}");
            response.EnsureSuccessStatusCode();
            var priceString = await response.Content.ReadAsStringAsync();
            return decimal.Parse(priceString, CultureInfo.InvariantCulture);
        }
    }
}
