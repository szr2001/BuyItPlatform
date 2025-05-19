using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.Dto;
using BuyItPlatform.GatewayApi.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BuyItPlatform.GatewayApi.Controllers
{
    [Route("gateway/userRatingApi")]
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
        public async Task<ResponseDto<UserRatingResponseDto>> GetUserRating(string targetUserId)
        {
            return await userRatingService.GetUserRating<UserRatingResponseDto>(targetUserId);
        }

        [HttpGet]
        [Route("deleteOfferedRatings/{targetUserId}")]
        public async Task<ResponseDto<object>> DeleteOfferedRatings(string targetUserId)
        {
            return await userRatingService.DeleteOfferedRatings<object>(targetUserId);
        }

        [HttpPost]
        [Route("rateUser")]
        public async Task<ResponseDto<object>> RateUser([FromBody] UserRatingRequestDto ratingRequest)
        {
            return await userRatingService.RateUser<object>(ratingRequest);
        }
    }
}
