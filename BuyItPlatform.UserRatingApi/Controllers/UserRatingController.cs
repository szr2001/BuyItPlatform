using BuyItPlatform.UserRatingApi.Models.Dto;
using BuyItPlatform.UserRatingApi.Services.IServices;
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

        public UserRatingController(IUserRatingService userRatingService, ITokenCookiesProvider tokenProvider, IJwtTokenHandler jwtTokenHandler)
        {
            this.userRatingService = userRatingService;
            this.tokenProvider = tokenProvider;
            this.jwtTokenHandler = jwtTokenHandler;
        }

        [HttpGet]
        [Route("getUsersScoreboard/{count:int}/{offset:int}")]
        public async Task<IActionResult> GetUsersScoreboard(int count, int offset)
        {
            try
            {
                response.Result = await userRatingService.GetUsersScoreboardAsync(count, offset);
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

        [HttpPost]
        [Route("rateUser")]
        public async Task<IActionResult> RateUser([FromBody] UserRatingRequestDto ratingRequest)
        {
            try
            {
                var Token = tokenProvider.GetToken();
                var tokenData = jwtTokenHandler.ExtractTokenData(Token);
                var Id = tokenData.Where(i => i.Type == "nameid").First().Value;
                ratingRequest.UserId = Id;

                await userRatingService.RateUserAsync(ratingRequest);
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
                response.Result = await userRatingService.GetUserRatingAsync(targetUserId);
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
                await userRatingService.DeleteOfferedRatingsAsync(targetUserId);
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
