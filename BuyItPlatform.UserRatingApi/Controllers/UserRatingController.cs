using BuyItPlatform.UserRatingApi.Models.Dto;
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
                await userRatingService.RateUser(userRating);
                response.Result = null;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message += ex.Message;
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
            }

            return Ok(response);
        }
    }
}
