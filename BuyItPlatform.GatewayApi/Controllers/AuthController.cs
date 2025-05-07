using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.Dto;
using BuyItPlatform.GatewayApi.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;

namespace BuyItPlatform.GatewayApi.Controllers
{
    [Route("gateway/authApi")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService authService;
        private readonly ITokensProvider tokenProvider;
        public AuthController(IAuthService authService, ITokensProvider tokenProvider)
        {
            this.authService = authService;
            this.tokenProvider = tokenProvider;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ResponseDto<object>> Register([FromBody] RegisterRequestDto registerData)
        {
            return await authService.RegisterUser<object>(registerData);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ResponseDto<UserDto>> Login([FromBody] LoginRequestDto loginData)
        {
            var result =  await authService.LoginUser<LoginResponseDto>(loginData);
            if (result.Success)
            { 
                //if the request was a success, get the tokens and save them in the cookies for the frontend
                tokenProvider.SetToken(result.Result?.Token!);
                tokenProvider.SetRefreshToken(result.Result?.RefreshToken!);
            }
            //and now return the userDto, without the tokens for security.
            return new ResponseDto<UserDto> {Success = result.Success, Result = result.Result!.User };
        }

        [HttpPost]
        [Route("assignRole")]
        public async Task<ResponseDto<object>> AssignRole([FromQuery] string email, [FromQuery] string roleName)
        {
            return await authService.AssignRole<object>(email, roleName);
        }

        [HttpGet]
        [Route("RefreshToken")]
        public async Task<ResponseDto<object>> RefreshToken()
        {
            ResponseDto<object> result = new();

            var tokenResult = await authService.RefreshToken<LoginResponseDto>();

            result.Success = tokenResult.Success;
            result.Message = tokenResult.Message;
            
            if (tokenResult.Success)
            {
                //if the request was a success, get the tokens and save them in the cookies for the frontend
                tokenProvider.SetToken(tokenResult.Result?.Token!);
                tokenProvider.SetRefreshToken(tokenResult.Result?.RefreshToken!);
            }

            return result;
        }

        [HttpGet]
        [Route("GetUserProfile/{userId}")]
        public async Task<ResponseDto<UserProfile>> GetUserProfile(string userId)
        {
            return await authService.GetUserProfile<UserProfile>(userId);
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<ResponseDto<object>> Logout()
        {
            return await authService.Logout<object>();
        }
    }
}
