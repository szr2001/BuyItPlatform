using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BuyItPlatform.GatewayApi.Controllers
{
    [Route("gateway/authApi")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ResponseDto> Register([FromBody] RegisterRequestDto registerData)
        {
            return await authService.RegisterUser(registerData);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ResponseDto> Login([FromBody] LoginRequestDto loginData)
        {
            return await authService.LoginUser(loginData);
        }
        [HttpPost]
        [Route("assignRole")]
        public async Task<ResponseDto> AssignRole([FromQuery] string email, [FromQuery] string roleName)
        {
            return await authService.AssignRole(email, roleName);
        }
    }
}
