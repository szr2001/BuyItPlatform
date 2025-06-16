using BuyItPlatform.GatewayApi.Models.AuthApiDto;
using BuyItPlatform.GatewayApi.Service.IService;
using BuyItPlatform.GatewayApi.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuyItPlatform.GatewayApi.Controllers
{
    [Route("gateway/authApi/user")]
    [Authorize]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IListingsService listingsService;
        private readonly IUserService userService;
        private readonly IUserRatingService userRatingService;
        public UserController(IUserService userService, IUserRatingService userRatingService, IListingsService listingsService)
        {
            this.userService = userService;
            this.userRatingService = userRatingService;
            this.listingsService = listingsService;
        }

        [HttpGet]
        [Route("getUserProfile/{userId}")]
        public async Task<IActionResult> GetUserProfile(string userId)
        {
            var apiResult = await userService.GetUserProfileAsync(userId);
            if (apiResult.Success && apiResult.Result != null)
            {
                var ratingResult = await userRatingService.GetUserRatingAsync(apiResult.Result.Id);
                if (ratingResult.Success && ratingResult.Result != null)
                {
                    apiResult.Result.AverageRating = ratingResult.Result.AverageRating;
                    apiResult.Result.NumberOfRatings = ratingResult.Result.NumberOfRatings;
                }

                var listingResult = await listingsService.GetUserListings(userId);
                if(listingResult.Success == true && listingResult.Result != null)
                {
                    apiResult.Result.Listings = listingResult.Result;
                }
            }
            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpPost]
        [Route("updateUserDesc/{desc}")]
        public async Task<IActionResult> UpdateUserDesc(string desc)
        {
            var apiResult = await userService.SetUserDescAsync(desc);
            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpPost]
        [Route("updateUserName/{name}")]
        public async Task<IActionResult> UpdateUserName(string name)
        {
            var apiResult = await userService.SetUserNameAsync(name);
            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpPost]
        [Route("updateUserPhoneNumber/{phoneNumber}")]
        public async Task<IActionResult> UpdateUserPhoneNumber(string phoneNumber)
        {
            var apiResult = await userService.SetUserPhoneNumberAsync(phoneNumber);
            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpPost]
        [Route("updateUserProfilePic")] // FromForm because we expect an image in Binary format
        public async Task<IActionResult> UpdateUserProfilePic([FromForm] ImageDto profilePic)
        {
            var apiResult = await userService.SetUserProfilePicAsync(profilePic);
            return StatusCode(apiResult.StatusCode, apiResult);
        }
    }
}
