using AutoMapper;
using BuyItPlatform.CommentsApi.Models.Dto;
using BuyItPlatform.CommentsApi.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuyItPlatform.CommentsApi.Controllers
{
    [ApiController]
    [Route("commentsApi")]
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;
        private readonly IMapper mapper;
        private IJwtTokenHandler jwtTokenHandler;
        private ITokenCookiesProvider tokenCookiesProvider;
        private ResponseDto response = new();

        public CommentsController(ICommentsService commentsService, IMapper mapper, ITokenCookiesProvider tokenCookiesProvider, IJwtTokenHandler jwtTokenHandler)
        {
            this.commentsService = commentsService;
            this.mapper = mapper;
            this.tokenCookiesProvider = tokenCookiesProvider;
            this.jwtTokenHandler = jwtTokenHandler;
        }
        [HttpGet]
        [Route("countListingComments/{listingId}")]
        public async Task<IActionResult> CountListingComments(string listingId)
        {
            try
            {
                response.Result = await commentsService.CountListingCommentsAsync(listingId);
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
        [Route("uploadComment")]
        public async Task<IActionResult> UploadComment([FromBody] CommentDto commentDto)
        {
            try
            {
                var Token = tokenCookiesProvider.GetToken();
                var tokenData = jwtTokenHandler.ExtractTokenData(Token);
                var userId = tokenData.Where(i => i.Type == "nameid").First().Value;
                commentDto.UserId = userId;

                await commentsService.UploadCommentAsync(commentDto);
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
        [HttpPost]
        [Route("deleteComment/{commentId}")]
        public async Task<IActionResult> DeleteComment(string commentId)
        {
            try
            {
                var Token = tokenCookiesProvider.GetToken();
                var tokenData = jwtTokenHandler.ExtractTokenData(Token);
                var userId = tokenData.Where(i => i.Type == "nameid").First().Value;
                await commentsService.DeleteCommentAsync(commentId, userId);
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
        [HttpPost]
        [Route("deleteUserComments/{userId}")]
        public async Task<IActionResult> DeleteUserComments(string userId)
        {
            try
            {
                await commentsService.DeleteUserCommentsAsync(userId);
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
        [HttpPost]
        [Route("deleteListingComments/{listingId}")]
        public async Task<IActionResult> DeleteListingComments(string listingId)
        {
            try
            {
                await commentsService.DeleteListingCommentsAsync(listingId);
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
        [Route("getListingComments/{listingId}/{count:int}/{offset:int}")]
        public async Task<IActionResult> GetListingComments(string listingId, int count, int offset)
        {
            try
            {
                var comments = await commentsService.GetListingCommentsAsync(listingId, count, offset);
                var commentsDto = mapper.Map<List<CommentDto>>(comments);
                response.Result = commentsDto;
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
        [Route("getUserComments/{userId}/{count:int}/{offset:int}")]
        public async Task<IActionResult> GetUserComments(string userId, int count, int offset)
        {
            try
            {
                var comments = await commentsService.GetUserCommentsAsync(userId, count, offset);
                var commentsDto = mapper.Map<List<CommentDto>>(comments);
                response.Result = commentsDto;
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
