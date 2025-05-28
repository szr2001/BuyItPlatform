using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.Dto;
using BuyItPlatform.GatewayApi.Service.IService;
using BuyItPlatform.GatewayApi.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace BuyItPlatform.GatewayApi.Controllers
{
    [Route("gateway/authApi/user")]
    [Authorize]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IUserRatingService userRatingService;
        public UserController(IUserService userService, IUserRatingService userRatingService)
        {
            this.userService = userService;
            this.userRatingService = userRatingService;
        }

        [HttpGet]
        [Route("getUserProfile/{userId}")]
        public async Task<IActionResult> GetUserProfile(string userId)
        {
            var apiResult = await userService.GetUserProfileAsync<UserProfileDto>(userId);
            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpPost]
        [Route("updateUserDesc/{desc}")]
        public async Task<IActionResult> UpdateUserDesc(string desc)
        {
            var apiResult = await userService.SetUserDescAsync<object>(desc);
            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpPost]
        [Route("updateUserName/{name}")]
        public async Task<IActionResult> UpdateUserName(string name)
        {
            var apiResult = await userService.SetUserNameAsync<object>(name);
            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpPost]
        [Route("updateUserPhoneNumber/{phoneNumber}")]
        public async Task<IActionResult> UpdateUserPhoneNumber(string phoneNumber)
        {
            var apiResult = await userService.SetUserPhoneNumberAsync<object>(phoneNumber);
            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpPost]
        [Route("updateUserProfilePic")] // FromForm because we expect an image in Binary format
        public async Task<IActionResult> UpdateUserProfilePic([FromForm] ImageDto profilePic)
        {
            var apiResult = await userService.SetUserProfilePicsAsync<string>(profilePic);
            return StatusCode(apiResult.StatusCode, apiResult);
        }
    }
}
