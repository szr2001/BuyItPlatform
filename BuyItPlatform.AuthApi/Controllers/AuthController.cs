using BuyItPlatform.AuthApi.Models.Dto;
using BuyItPlatform.AuthApi.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace BuyItPlatform.AuthApi.Controllers
{
    [Route("authApi")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService authService;
        private ResponseDto response = new();

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ResponseDto> Register([FromBody] RegisterRequestDto registerData)
        {
            try
            {
                await authService.RegisterUser(registerData);
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
                response.Success= true;
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
