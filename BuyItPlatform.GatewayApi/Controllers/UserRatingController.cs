using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.Dto;
using BuyItPlatform.GatewayApi.Service.IService;
using BuyItPlatform.GatewayApi.Services;
using BuyItPlatform.GatewayApi.Services.IServices;
using BuyItPlatform.GatewayApi.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuyItPlatform.GatewayApi.Controllers
{
    [Route("gateway/userRatingApi")]
    [Authorize]
    [ApiController]
    public class UserRatingController : Controller
    {
        private readonly IUserService userService;
        private readonly IAuthService authService;
        private readonly IUserRatingService userRatingService;
        public UserRatingController(IUserRatingService userRatingService, IAuthService authService, IUserService userService)
        {
            this.userRatingService = userRatingService;
            this.authService = authService;
            this.userService = userService;
        }

        [HttpGet]
        [Route("getUsersScoreboard")]
        public async Task<IActionResult> GetUsersScoreboard()
        {
            var scoreboardResult = await userRatingService.GetUsersScoreboardAsync<UserRatingResponseDto[]>(20, 0);

            if (!scoreboardResult.Success || scoreboardResult.Result == null)
            {
                return StatusCode(scoreboardResult.StatusCode, scoreboardResult);
            }

            var userIds = scoreboardResult.Result.Select(u => u.TargetUserId).ToArray();
            var apiResult = await userService.GetUsersProfilesAsync<UserProfileDto[]>(userIds);

            if (!apiResult.Success || apiResult.Result == null)
            {
                return StatusCode(apiResult.StatusCode, apiResult);
            }

            //create a dictionary for lookups
            var ratingLookup = scoreboardResult.Result.ToDictionary(
                x => x.TargetUserId, //key
                x => new { x.AverageRating, x.NumberOfRatings }); //value

            //loop through the userProfileDto's
            foreach (var profile in apiResult.Result)
            {
                //check the profile id against the lookup table containing the data from the
                //userRating api and if the id match we set the userProfile rating data
                if (ratingLookup.TryGetValue(profile.Id, out var ratingInfo))
                {
                    profile.AverageRating = ratingInfo.AverageRating;
                    profile.NumberOfRatings = ratingInfo.NumberOfRatings;
                }
            }
            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpGet]
        [Route("getUserRating/{targetUserId}")]
        public async Task<IActionResult> GetUserRating(string targetUserId)
        {
            var userIds = await authService.IsUserIdPresent<object>(targetUserId);

            if (!userIds.Success)
            {
                return StatusCode(userIds.StatusCode, userIds);
            }

            var apiResult = await userRatingService.GetUserRatingAsync<UserRatingResponseDto>(targetUserId);
            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpGet]
        [Route("deleteOfferedRatings/{targetUserId}")]
        public async Task<IActionResult> DeleteOfferedRatings(string targetUserId)
        {
            var apiResult = await userRatingService.DeleteOfferedRatingsAsync<object>(targetUserId);
            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpPost]
        [Route("rateUser")]
        public async Task<IActionResult> RateUser([FromBody] UserRatingRequestDto ratingRequest)
        {
            var userIdResult = await authService.IsUserIdPresent<object>(ratingRequest.TargetUserId);

            if (!userIdResult.Success)
            {
                return StatusCode(userIdResult.StatusCode, userIdResult);
            }

            var apiResult = await userRatingService.RateUserAsync<object>(ratingRequest);
            return StatusCode(apiResult.StatusCode, apiResult);
        }
    }
}
