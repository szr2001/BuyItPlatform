using BuyItPlatform.UserRatingApi.Models.Dto;
using BuyItPlatform.UserRatingApi.Service.IService;
using BuyItPlatform.UserRatingApi.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuyItPlatform.UserRatingApi.Controllers
{
    [Route("userRatingApi")]
    [Authorize]
    [ApiController]
    public class UserRatingController : Controller
    {
        private readonly IUserRatingService userRatingService;
        private readonly ITokenCookiesProvider tokenProvider;
        private readonly IJwtTokenHandler jwtTokenHandler;
        private ResponseDto response = new();

        public UserRatingController(IUserRatingService userRatingService)
        {
            this.userRatingService = userRatingService;
        }

        [HttpPost]
        [Route("rateUser")]
        public async Task<IActionResult> RateUser([FromBody] UserRatingRequestDto userRating)
        {
            try
            {
                var Token = tokenProvider.GetToken();
                var tokenData = jwtTokenHandler.ExtractTokenData(Token);
                var Id = tokenData.Where(i => i.Type == "nameid").First().Value;
                userRating.UserId = Id;

                await userRatingService.RateUser(userRating);
                response.Result = null;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message += ex.Message;
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("getUserRating/{targetUserId}")]
        public async Task<IActionResult> GetUserRating(string targetUserId)
        {
            try
            {
                response.Result = await userRatingService.GetUserRating(targetUserId);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message += ex.Message;
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("deleteOfferedRatings/{targetUserId}")]
        public async Task<IActionResult> DeleteOfferedRatings(string targetUserId)
        {
            try
            {
                await userRatingService.DeleteOfferedRatings(targetUserId);
                response.Result = null;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message += ex.Message;
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
