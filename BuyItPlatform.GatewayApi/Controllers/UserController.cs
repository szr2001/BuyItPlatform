using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.Dto;
using BuyItPlatform.GatewayApi.Service;
using BuyItPlatform.GatewayApi.Service.IService;
using BuyItPlatform.GatewayApi.Services.IServices;
using BuyItPlatform.GatewayApi.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BuyItPlatform.GatewayApi.Controllers
{
    [Route("gateway/authApi/user")]
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
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("updateUserPhoneNumber/{phoneNumber}")]
        public async Task<ResponseDto<object>> UpdateUserPhoneNumber(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("updateUserProfilePic")]
        public async Task<ResponseDto<object>> UpdateUserProfilePic([FromBody] IFormFile profilePic)
        {
            throw new NotImplementedException();
        }
    }
}
