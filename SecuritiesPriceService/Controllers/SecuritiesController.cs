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
