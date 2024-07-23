using BNP.SecuritiesPriceService.Models;
using BNP.SecuritiesPriceService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BNP.SecuritiesPriceService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecuritiesController : ControllerBase
    {
        private readonly ISecurityService _securityService;

        public SecuritiesController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        /// <summary>
        /// Retrieves and stores prices for the provided list of ISINs.
        /// </summary>
        /// <param name="isins">A list of ISINs for which the prices are to be retrieved.</param>
        /// <returns>A response indicating the success or failure of the operation.</returns>
        /// <response code="200">Prices retrieved and stored successfully.</response>
        /// <response code="500">Internal server error if something goes wrong.</response>
        [HttpPost("retrieve-prices")]
        public async Task<IActionResult> RetrievePrices([FromBody] List<string> isins)
        {
            try
            {
                await _securityService.RetrieveAndStorePricesAsync(isins);
                return Ok("Prices retrieved and stored successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the current price for the specified ISIN.
        /// </summary>
        /// <param name="isin">The ISIN for which to retrieve the price.</param>
        /// <returns>The current price for the specified ISIN.</returns>
        /// <response code="200">Returns the current price for the specified ISIN.</response>
        /// <response code="500">Internal server error if something goes wrong.</response>
        [HttpGet("price/{isin}")]
        public async Task<IActionResult> GetPrice(string isin)
        {
            try
            {
                var price = await _securityService.GetPriceByIsinAsync(isin);
                return Ok(new { ISIN = isin, Price = price });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Saves a new security to the database.
        /// </summary>
        /// <param name="security">The security object to be saved.</param>
        /// <returns>A response indicating the success or failure of the operation.</returns>
        /// <response code="200">Security saved successfully.</response>
        /// <response code="400">Bad request if the security data is invalid.</response>
        /// <response code="500">Internal server error if something goes wrong.</response>
        [HttpPost("save-security")]
        public async Task<IActionResult> SaveSecurity([FromBody] Security security)
        {
            try
            {
                if (security == null || string.IsNullOrWhiteSpace(security.ISIN))
                {
                    return BadRequest("Invalid security data.");
                }

                await _securityService.SaveSecurityAsync(security);
                return Ok("Security saved successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
