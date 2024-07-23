using BNP.SecuritiesPriceService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BNP.SecuritiesPriceService.Services
{
    public interface ISecurityService
    {
        /// <summary>
        /// Retrieves and stores the price for each ISIN in the provided list.
        /// </summary>
        /// <param name="isins">A list of ISINs for which to retrieve and store prices.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task RetrieveAndStorePricesAsync(IEnumerable<string> isins);

        /// <summary>
        /// Gets the current price for a specific ISIN from an external API.
        /// </summary>
        /// <param name="isin">The ISIN for which to retrieve the price.</param>
        /// <returns>The current price for the specified ISIN.</returns>
        Task<decimal> GetPriceByIsinAsync(string isin);

        /// <summary>
        /// Saves a single security record in the database.
        /// </summary>
        /// <param name="security">The security to be saved.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SaveSecurityAsync(Security security);
    }
}
