using AutoMapper;
using BuyItPlatform.GatewayApi.Models.CommentsApiDto;
using BuyItPlatform.GatewayApi.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuyItPlatform.GatewayApi.Controllers
{
    [Route("gateway/commentsApi")]
    [Authorize]
    [ApiController]
    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        [HttpPost]
        [Route("uploadCommentAsync")]
        public async Task<IActionResult> UploadCommentAsync([FromBody] CommentDto commentDto)
        {
            var apiResult = await commentsService.UploadCommentAsync(commentDto);
            return StatusCode(apiResult.StatusCode, apiResult);
        }
        [HttpPost]
        [Route("deleteCommentAsync/{commentId}")]
        public async Task<IActionResult> DeleteCommentAsync(string commentId)
        {
            var apiResult = await commentsService.DeleteCommentAsync(commentId);
            return StatusCode(apiResult.StatusCode, apiResult);
        }
        [HttpPost]
        [Route("deleteUserCommentsAsync/{userId}")]
        public async Task<IActionResult> DeleteUserCommentsAsync(string userId)
        {
            var apiResult = await commentsService.DeleteUserCommentsAsync(userId);
            return StatusCode(apiResult.StatusCode, apiResult);
        }
        [HttpPost]
        [Route("deleteListingCommentsAsync/{listingId}")]
        public async Task<IActionResult> DeleteListingCommentsAsync(string listingId)
        {
            var apiResult = await commentsService.DeleteListingCommentsAsync(listingId);
            return StatusCode(apiResult.StatusCode, apiResult);
        }
        [HttpGet]
        [Route("getListingCommentsAsync/{listingId}")]
        public async Task<IActionResult> GetListingCommentsAsync(string listingId)
        {
            var apiResult = await commentsService.GetListingCommentsAsync(listingId);
            return StatusCode(apiResult.StatusCode, apiResult);

        }
        [HttpGet]
        [Route("getUserCommentsAsync/{userId}")]
        public async Task<IActionResult> GetUserCommentsAsync(string userId)
        {
            var apiResult = await commentsService.GetUserCommentsAsync(userId);
            return StatusCode(apiResult.StatusCode, apiResult);
        }
    }
}
