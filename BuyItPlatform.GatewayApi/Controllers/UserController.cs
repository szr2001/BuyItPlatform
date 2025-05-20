using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.Dto;
using BuyItPlatform.GatewayApi.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuyItPlatform.GatewayApi.Controllers
{
    [Route("gateway/authApi/user")]
    [Authorize]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [Route("getUserProfile/{userId}")]
        public async Task<IActionResult> GetUserProfile(string userId)
        {
            return Ok(await userService.GetUserProfileAsync<UserProfileDto>(userId));
        }

        [HttpPost]
        [Route("updateUserDesc/{desc}")]
        public async Task<IActionResult> UpdateUserDesc(string desc)
        {
            return Ok(await userService.UpdateUserDescAsync<object>(desc));
        }

        [HttpPost]
        [Route("updateUserName/{name}")]
        public async Task<IActionResult> UpdateUserName(string name)
        {
            return Ok(await userService.UpdateUserNameAsync<object>(name));
        }

        [HttpPost]
        [Route("updateUserPhoneNumber/{phoneNumber}")]
        public async Task<IActionResult> UpdateUserPhoneNumber(string phoneNumber)
        {
            return Ok(await userService.UpdateUserPhoneNumberAsync<object>(phoneNumber));
        }

        [HttpPost]
        [Route("updateUserProfilePic")] // FromForm because we expect an image in Binary format
        public async Task<IActionResult> UpdateUserProfilePic([FromForm] ImageDto profilePic)
        {
            return Ok(await userService.UpdateUserProfilePicsAsync<string>(profilePic));
        }
    }
}
