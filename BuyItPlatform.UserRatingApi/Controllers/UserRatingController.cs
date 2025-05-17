using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuyItPlatform.UserRatingApi.Controllers
{
    [Route("userRatingApi")]
    [Authorize]
    [ApiController]
    public class UserRatingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
