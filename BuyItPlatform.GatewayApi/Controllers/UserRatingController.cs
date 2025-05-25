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
        [Route("getUsersScoreboard/{count:int}/{offset:int}")]
        public async Task<IActionResult> GetUsersScoreboard()
        {
            var apiResult = await userRatingService.GetUsersScoreboard<UserRatingResponseDto>(10,0);
            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpGet]
        [Route("getUserRating/{targetUserId}")]
        public async Task<IActionResult> GetUserRating(string targetUserId)
        {
            var apiResult = await userRatingService.GetUserRating<UserRatingResponseDto>(targetUserId);
            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpGet]
        [Route("deleteOfferedRatings/{targetUserId}")]
        public async Task<IActionResult> DeleteOfferedRatings(string targetUserId)
        {
            var apiResult = await userRatingService.DeleteOfferedRatings<object>(targetUserId);
            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpPost]
        [Route("rateUser")]
        public async Task<IActionResult> RateUser([FromBody] UserRatingRequestDto ratingRequest)
        {
            var apiResult = await userRatingService.RateUser<object>(ratingRequest);
            return StatusCode(apiResult.StatusCode, apiResult);
        }
    }
}
