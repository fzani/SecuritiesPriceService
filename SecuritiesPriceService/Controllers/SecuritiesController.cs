
using BNP.SecuritiesPriceService.Services;
using Microsoft.AspNetCore.Mvc;

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
            await _securityService.RetrieveAndStorePricesAsync(isins);
            return Ok();
        }
    }
}
