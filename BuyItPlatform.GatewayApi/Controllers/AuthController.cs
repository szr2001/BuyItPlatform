using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.Dto;
using BuyItPlatform.GatewayApi.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;

namespace BuyItPlatform.GatewayApi.Controllers
{
    [Route("gateway/authApi/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService authService;
        private readonly ITokenCookiesProvider tokenProvider;
        public AuthController(IAuthService authService, ITokenCookiesProvider tokenProvider)
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
            ResponseDto<UserDto> returnResponse = new();
            var result =  await authService.LoginUser<LoginResponseDto>(loginData);
            if (result.Success)
            { 
                //if the request was a success, get the tokens and save them in the cookies for the frontend
                tokenProvider.SetToken(result.Result!.Token!);
                tokenProvider.SetRefreshToken(result.Result?.RefreshToken!);
                returnResponse.Result = result.Result!.User;
            }
            
            returnResponse.Success = result.Success;
            returnResponse.Message = result.Message;

            return returnResponse;
        }

        //[HttpPost]
        //[Route("assignRole")]
        //public async Task<ResponseDto<object>> AssignRole([FromQuery] string email, [FromQuery] string roleName)
        //{
        //    return await authService.AssignRole<object>(email, roleName);
        //}

        [HttpGet]
        [Route("refreshToken")]
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

        [HttpPost]
        [Route("logout")]
        public async Task<ResponseDto<object>> Logout()
        {
            var result = await authService.Logout<object>();
            if(result.Success)
            {
                tokenProvider.ClearTokens();
            }

            return result;
        }
    }
}
