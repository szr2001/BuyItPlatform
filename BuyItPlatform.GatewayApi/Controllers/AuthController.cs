﻿using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.AuthApiDto;
using BuyItPlatform.GatewayApi.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var apiResult = await authService.RegisterUserAsync(registerData);
            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginData)
        {
            MicroserviceResponseDto<UserDto> apiResult = new();
            var loginResult =  await authService.LoginUserAsync(loginData);
            if (loginResult.Success)
            { 
                //if the request was a success, get the tokens and save them in the cookies for the frontend
                tokenProvider.SetToken(loginResult.Result!.Token!);
                tokenProvider.SetRefreshToken(loginResult.Result.RefreshToken!);
                apiResult.Result = loginResult.Result.User;
            }
            
            apiResult.Success = loginResult.Success;
            apiResult.Message = loginResult.Message;
            apiResult.StatusCode = loginResult.StatusCode;

            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpGet]
        [Route("refreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            MicroserviceResponseDto<object> apiResult = new();

            var tokenResult = await authService.RefreshTokenAsync();
            
            if (tokenResult.Success)
            {
                //if the request was a success, get the tokens and save them in the cookies for the frontend
                tokenProvider.SetToken(tokenResult.Result?.Token!);
                tokenProvider.SetRefreshToken(tokenResult.Result?.RefreshToken!);
            }

            apiResult.Success = tokenResult.Success;
            apiResult.Message = tokenResult.Message;
            apiResult.StatusCode = tokenResult.StatusCode;

            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpGet]
        [Authorize]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            var apiResult = await authService.LogoutAsync();
            if(apiResult.Success)
            {
                tokenProvider.ClearTokens();
            }
            return StatusCode(apiResult.StatusCode, apiResult);
        }
    }
}
