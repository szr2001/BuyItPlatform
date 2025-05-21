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
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerData)
        {
            var apiResult = await authService.RegisterUser<object>(registerData);
            return StatusCode((int)apiResult.StatusCode, apiResult);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginData)
        {
            MicroserviceResponseDto<UserDto> apiResult = new();
            var result =  await authService.LoginUser<LoginResponseDto>(loginData);
            if (result.Success)
            { 
                //if the request was a success, get the tokens and save them in the cookies for the frontend
                tokenProvider.SetToken(result.Result!.Token!);
                tokenProvider.SetRefreshToken(result.Result?.RefreshToken!);
                apiResult.Result = result.Result!.User;
            }
            
            apiResult.Success = result.Success;
            apiResult.Message = result.Message;

            return StatusCode((int)apiResult.StatusCode, apiResult);
        }

        //[HttpPost]
        //[Route("assignRole")]
        //public async Task<IActionResult> AssignRole([FromQuery] string email, [FromQuery] string roleName)
        //{
        //    return Ok(await authService.AssignRole<object>(email, roleName));
        //}

        [HttpGet]
        [Route("refreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            MicroserviceResponseDto<object> apiResult = new();

            var tokenResult = await authService.RefreshToken<LoginResponseDto>();

            apiResult.Success = tokenResult.Success;
            apiResult.Message = tokenResult.Message;
            
            if (tokenResult.Success)
            {
                //if the request was a success, get the tokens and save them in the cookies for the frontend
                tokenProvider.SetToken(tokenResult.Result?.Token!);
                tokenProvider.SetRefreshToken(tokenResult.Result?.RefreshToken!);
            }

            return StatusCode((int)apiResult.StatusCode, apiResult);
        }

        [HttpPost]
        [Authorize]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            var apiResult = await authService.Logout<object>();
            if(apiResult.Success)
            {
                tokenProvider.ClearTokens();
            }
            return StatusCode((int)apiResult.StatusCode, apiResult);
        }
    }
}
