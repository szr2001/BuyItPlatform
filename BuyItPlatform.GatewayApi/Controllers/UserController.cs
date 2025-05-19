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
        public async Task<ResponseDto<UserProfileDto>> GetUserProfile(string userId)
        {
            return await userService.GetUserProfileAsync<UserProfileDto>(userId);
        }

        [HttpPost]
        [Route("updateUserDesc/{desc}")]
        public async Task<ResponseDto<object>> UpdateUserDesc(string desc)
        {
            return await userService.UpdateUserDescAsync<object>(desc);
        }

        [HttpPost]
        [Route("updateUserName/{name}")]
        public async Task<ResponseDto<object>> UpdateUserName(string name)
        {
            return await userService.UpdateUserNameAsync<object>(name);
        }

        [HttpPost]
        [Route("updateUserPhoneNumber/{phoneNumber}")]
        public async Task<ResponseDto<object>> UpdateUserPhoneNumber(string phoneNumber)
        {
            return await userService.UpdateUserPhoneNumberAsync<object>(phoneNumber);
        }

        [HttpPost]
        [Route("updateUserProfilePic")] // FromForm because we expect an image in Binary format
        public async Task<ResponseDto<string>> UpdateUserProfilePic([FromForm] ImageDto profilePic)
        {
            return await userService.UpdateUserProfilePicsAsync<string>(profilePic);
        }
    }
}
