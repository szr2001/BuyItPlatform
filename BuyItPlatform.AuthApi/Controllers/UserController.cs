using BuyItPlatform.AuthApi.Models.Dto;
using BuyItPlatform.AuthApi.Service.IService;
using BuyItPlatform.AuthApi.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuyItPlatform.AuthApi.Controllers
{
    [Route("authApi/user")]
    [Authorize]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly ITokensProvider tokenProvider;
        private ResponseDto response = new();

        public UserController(IUserService userService, ITokensProvider tokenProvider)
        {
            this.userService = userService;
            this.tokenProvider = tokenProvider;
        }


        [HttpGet]
        [Route("GetUserProfile/{userId}")]
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
    }
}
