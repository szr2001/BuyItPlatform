using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.Dto;
using BuyItPlatform.GatewayApi.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuyItPlatform.GatewayApi.Controllers
{
    [Route("gateway/userRatingApi")]
    [Authorize]
    [ApiController]
    public class UserRatingController : Controller
    {
        private readonly IUserRatingService userRatingService;

        public UserRatingController(IUserRatingService userRatingService)
        {
            this.userRatingService = userRatingService;
        }

        [HttpGet]
        [Route("getUserRating/{targetUserId}")]
        public async Task<IActionResult> GetUserRating(string targetUserId)
        {
            return Ok(await userRatingService.GetUserRating<UserRatingResponseDto>(targetUserId));
        }

        [HttpGet]
        [Route("deleteOfferedRatings/{targetUserId}")]
        public async Task<IActionResult> DeleteOfferedRatings(string targetUserId)
        {
            return Ok(await userRatingService.DeleteOfferedRatings<object>(targetUserId));
        }

        [HttpPost]
        [Route("rateUser")]
        public async Task<IActionResult> RateUser([FromBody] UserRatingRequestDto ratingRequest)
        {
            return Ok(await userRatingService.RateUser<object>(ratingRequest));
        }
    }
}
