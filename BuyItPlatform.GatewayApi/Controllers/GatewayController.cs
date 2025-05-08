using Microsoft.AspNetCore.Mvc;

namespace BuyItPlatform.GatewayApi.Controllers
{
    [Route("gateway")]
    [ApiController]
    public class GatewayController : Controller
    {
        [HttpGet]
        [Route("Ping")]
        public IActionResult Ping()
        {
            return Ok();
        }
    }
}
