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
        private readonly ITokenCookiesProvider tokenProvider;
        private ResponseDto response = new();

        public AuthController(IAuthService authService, ITokenCookiesProvider tokenProvider)
        {
            this.tokenProvider = tokenProvider;
            this.authService = authService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerData)
        {
            try
            {
                await authService.RegisterUserAsync(registerData);
                response.Result = null;
                response.Success = true;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginData)
        {
            try
            {
                response.Result = await authService.LoginUserAsync(loginData);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("refreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                var refreshToken = tokenProvider.GetRefreshToken();

                response.Result = await authService.RefreshTokenAsync(refreshToken!);
                response.Success = true;
            }
            catch (UnauthorizedAccessException ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
                return Unauthorized(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
                return BadRequest(response);
            }
            return Ok(response);
        }

        //[HttpPost]
        //[Authorize]
        //[Route("assignRole")]
        //public async Task<IActionResult> AssignRole([FromQuery] string email, [FromQuery] string roleName)
        //{
        //    try
        //    {
        //        await authService.AssignRole(email, roleName);
        //        response.Result = null;
        //        response.Success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Success = false;
        //        response.Message = $"{ex.Message}";
        //        return BadRequest(response);
        //    }
        //    return Ok(response);
        //}

        [HttpGet]
        [Authorize]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var refreshToken = tokenProvider.GetRefreshToken();

                await authService.LogoutAsync(refreshToken);
                response.Result = null;
                response.Success = true;
            }
            catch (UnauthorizedAccessException ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
                return Unauthorized(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
