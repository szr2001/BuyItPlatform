using BuyItPlatform.AuthApi.Models.Dto;
using BuyItPlatform.AuthApi.Service.IService;
using BuyItPlatform.AuthApi.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BuyItPlatform.AuthApi.Controllers
{
    [Route("authApi/user")]
    [Authorize]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly ITokenCookiesProvider tokenProvider;
        private readonly IJwtTokenHandler jwtTokenHandler;
        private ResponseDto response = new();

        public UserController(IUserService userService, ITokenCookiesProvider tokenProvider, IJwtTokenHandler jwtTokenHandler)
        {
            this.userService = userService;
            this.tokenProvider = tokenProvider;
            this.jwtTokenHandler = jwtTokenHandler;
        }

        [HttpGet]
        [Route("getUserProfile/{userId}")]
        public async Task<IActionResult> GetUserProfile(string userId)
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
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("updateUserDesc/{desc}")]
        public async Task<IActionResult> UpdateUserDesc(string desc)
        {
            try
            {
                var Token = tokenProvider.GetToken();
                var tokenData = jwtTokenHandler.ExtractTokenData(Token);
                var Id = tokenData.Where(i => i.Type == "nameid").First().Value;
                await userService.UpdateUserDesc(Id, desc);
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

        [HttpPost]
        [Route("updateUserName/{name}")]
        public async Task<IActionResult> UpdateUserName(string name)
        {
            try
            {
                var Token = tokenProvider.GetToken();
                var tokenData = jwtTokenHandler.ExtractTokenData(Token);
                var Id = tokenData.Where(i => i.Type == "nameid").First().Value;
                await userService.UpdateUserName(Id, name);
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

        [HttpPost]
        [Route("updateUserPhoneNumber/{phoneNumber}")]
        public async Task<IActionResult> UpdateUserPhoneNumber(string phoneNumber)
        {
            try
            {
                var Token = tokenProvider.GetToken();
                var tokenData = jwtTokenHandler.ExtractTokenData(Token);
                var Id = tokenData.Where(i => i.Type == "nameid").First().Value;
                await userService.UpdateUserPhoneNumber(Id, phoneNumber);
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

        [HttpPost]
        [Route("updateUserProfilePic")]
        public async Task<IActionResult> UpdateUserProfilePic([FromForm] ImageDto profilePic)
        {
            try
            {
                var Token = tokenProvider.GetToken();
                var tokenData = jwtTokenHandler.ExtractTokenData(Token);
                var Id = tokenData.Where(i => i.Type == "nameid").First().Value;

                response.Result = await userService.UpdateUserProfilePic(Id, profilePic);
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

        [HttpPost]
        [Route("areUserIdsPresent")]
        public async Task<IActionResult> AreUserIdsPresent([FromBody] string[] userIds)
        {
            try
            {
                await userService.AreUserIdsPresent(userIds);
                response.Result = null;
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
    }
}
