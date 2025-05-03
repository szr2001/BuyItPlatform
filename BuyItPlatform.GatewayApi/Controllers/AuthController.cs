using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.Dto;
using BuyItPlatform.GatewayApi.Services.IServices;
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
                tokenProvider.SetTokens(result.Result?.Token!, result.Result?.RefreshToken!);
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

        [HttpPost]
        [Route("refreshToken")]
        public async Task<ResponseDto<UserDto>> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var result = await authService.RefreshToken<LoginResponseDto>(request);
            if (result.Success)
            {
                tokenProvider.SetTokens(result.Result?.Token!, result.Result?.RefreshToken!);
            }
            return new ResponseDto<UserDto> { Success = result.Success, Result = result.Result!.User };
        }
    }
}
