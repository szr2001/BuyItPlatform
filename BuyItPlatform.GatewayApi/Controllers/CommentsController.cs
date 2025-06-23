using AutoMapper;
using BuyItPlatform.GatewayApi.Models.AuthApiDto;
using BuyItPlatform.GatewayApi.Models.CommentsApiDto;
using BuyItPlatform.GatewayApi.Service.IService;
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
        private readonly IUserService userService;

        public CommentsController(ICommentsService commentsService, IUserService userService)
        {
            this.commentsService = commentsService;
            this.userService = userService;
        }

        [HttpPost]
        [Route("uploadComment")]
        public async Task<IActionResult> UploadComment([FromBody] CommentUploadDto commentDto)
        {
            var apiResult = await commentsService.UploadCommentAsync(commentDto);
            return StatusCode(apiResult.StatusCode, apiResult);
        }
        [HttpPost]
        [Route("deleteComment/{commentId}")]
        public async Task<IActionResult> DeleteComment(string commentId)
        {
            var apiResult = await commentsService.DeleteCommentAsync(commentId);
            return StatusCode(apiResult.StatusCode, apiResult);
        }
        [HttpPost]
        [Route("deleteUserComments/{userId}")]
        public async Task<IActionResult> DeleteUserComments(string userId)
        {
            var apiResult = await commentsService.DeleteUserCommentsAsync(userId);
            return StatusCode(apiResult.StatusCode, apiResult);
        }
        [HttpPost]
        [Route("deleteListingComments/{listingId}")]
        public async Task<IActionResult> DeleteListingComments(string listingId)
        {
            var apiResult = await commentsService.DeleteListingCommentsAsync(listingId);
            return StatusCode(apiResult.StatusCode, apiResult);
        }
        [HttpGet]
        [Route("getListingComments/{listingId}/{count:int}/{offset:int}")]
        public async Task<IActionResult> GetListingComments(string listingId, int count, int offset)
        {
            var apiResult = await commentsService.GetListingCommentsAsync(listingId, count, offset);
            if (apiResult.Success && apiResult.Result != null)
            {
                var profilesResult = await userService.GetUsersProfilesAsync(apiResult.Result.Select(i => i.UserId).ToArray());
                if(profilesResult.Success && profilesResult.Result != null)
                {
                    Dictionary<string, UserProfileDto> profiles = profilesResult.Result.ToDictionary(i => i.Id, i => i);
                    foreach(var comment in apiResult.Result)
                    {
                        if (profiles.TryGetValue(comment.UserId, out var userProfile))
                        {
                            comment.userName = userProfile.UserName;
                            comment.userProfilePic = userProfile.ProfileImgLink;
                        }
                    }
                }
            }
            // do api call to userApi to get username and userprofile pic
            return StatusCode(apiResult.StatusCode, apiResult);

        }
        [HttpGet]
        [Route("getUserComments/{userId}/{count:int}/{offset:int}")]
        public async Task<IActionResult> GetUserComments(string userId, int count, int offset)
        {
            var apiResult = await commentsService.GetUserCommentsAsync(userId, count, offset);
            if (apiResult.Success && apiResult.Result != null)
            {
                var profilesResult = await userService.GetUsersProfilesAsync(apiResult.Result.Select(i => i.UserId).ToArray());
                if (profilesResult.Success && profilesResult.Result != null)
                {
                    Dictionary<string, UserProfileDto> profiles = profilesResult.Result.ToDictionary(i => i.Id, i => i);
                    foreach (var comment in apiResult.Result)
                    {
                        if (profiles.TryGetValue(comment.UserId, out var userProfile))
                        {
                            comment.userName = userProfile.UserName;
                            comment.userProfilePic = userProfile.ProfileImgLink;
                        }
                    }
                }
            }
            // do api call to userApi to get username and userprofile pic
            return StatusCode(apiResult.StatusCode, apiResult);
        }
    }
}
