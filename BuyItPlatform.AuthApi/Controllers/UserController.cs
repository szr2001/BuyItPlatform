using BuyItPlatform.AuthApi.Models.Dto;
using BuyItPlatform.AuthApi.Service.IService;
using BuyItPlatform.AuthApi.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BuyItPlatform.AuthApi.Controllers
{
    [Route("authApi/user")]
    [Authorize]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly ITokenCookiesProvider tokenProvider;
        private readonly IJwtTokenHandler jwtTokenHandler;
        private ResponseDto response = new();

        public UserController(IUserService userService, ITokenCookiesProvider tokenProvider, IJwtTokenHandler jwtTokenHandler)
        {
            this.userService = userService;
            this.tokenProvider = tokenProvider;
            this.jwtTokenHandler = jwtTokenHandler;
        }

        [HttpGet]
        [Route("getUserProfile/{userId}")]
        public async Task<ResponseDto> GetUserProfile(string userId)
        {
            try
            {
                response.Result = await userService.GetUserProfile(userId);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
            }
            return response;
        }

        [HttpPost]
        [Route("updateUserDesc/{desc}")]
        public async Task<ResponseDto> UpdateUserDesc(string desc)
        {
            try
            {
                var Token = tokenProvider.GetToken();
                var tokenData = jwtTokenHandler.ExtractTokenData(Token);
                var Id = tokenData.Where(i => i.Type == "nameid").First().Value;
                await userService.UpdateUserDesc(Id, desc);
                response.Result = null;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
            }
            return response;
        }

        [HttpPost]
        [Route("updateUserName/{name}")]
        public async Task<ResponseDto> UpdateUserName(string name)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("updateUserPhoneNumber/{phoneNumber}")]
        public async Task<ResponseDto> UpdateUserPhoneNumber(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("updateUserProfilePic")]
        public async Task<ResponseDto> UpdateUserProfilePic([FromBody]IFormFile profilePic)
        {
            throw new NotImplementedException();
        }
    }
}
