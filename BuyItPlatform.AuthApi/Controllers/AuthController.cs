using BuyItPlatform.AuthApi.Models.Dto;
using BuyItPlatform.AuthApi.Service.IService;
using BuyItPlatform.AuthApi.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuyItPlatform.AuthApi.Controllers
{
    [Route("authApi/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService authService;
        private readonly ITokensProvider tokenProvider;
        private ResponseDto response = new();

        public AuthController(IAuthService authService, ITokensProvider tokenProvider)
        {
            this.tokenProvider = tokenProvider;
            this.authService = authService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ResponseDto> Register([FromBody] RegisterRequestDto registerData)
        {
            try
            {
                await authService.RegisterUser(registerData);
                response.Result = null;
                response.Success = true;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
            }
            return response;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ResponseDto> Login([FromBody] LoginRequestDto loginData)
        {
            try
            {
                response.Result = await authService.LoginUser(loginData);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
            }
            return response;
        }

        [HttpGet]
        [Route("RefreshToken")]
        public async Task<ResponseDto> RefreshToken()
        {
            try
            {
                var refreshToken = tokenProvider.GetRefreshToken();

                response.Result = await authService.RefreshToken(refreshToken!);
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
        [Route("assignRole")]
        public async Task<ResponseDto> AssignRole([FromQuery] string email, [FromQuery] string roleName)
        {
            try
            {
                await authService.AssignRole(email,roleName);
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
        [Authorize]
        [Route("Logout")]
        public async Task<ResponseDto> Logout()
        {
            try
            {
                var refreshToken = tokenProvider.GetRefreshToken();

                await authService.Logout(refreshToken);
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
    }
}
