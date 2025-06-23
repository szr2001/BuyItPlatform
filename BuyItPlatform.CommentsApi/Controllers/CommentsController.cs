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

        [HttpPost]
        [Route("uploadCommentAsync")]
        public async Task<IActionResult> UploadCommentAsync([FromBody] CommentDto commentDto)
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
        [Route("deleteCommentAsync/{commentId}")]
        public async Task<IActionResult> DeleteCommentAsync(string commentId)
        {
            try
            {
                await commentsService.DeleteCommentAsync(commentId);
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
        [Route("deleteUserCommentsAsync/{userId}")]
        public async Task<IActionResult> DeleteUserCommentsAsync(string userId)
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
        [Route("deleteListingCommentsAsync/{listingId}")]
        public async Task<IActionResult> DeleteListingCommentsAsync(string listingId)
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
        [Route("getListingCommentsAsync/{listingId}/{count:int}/{offset:int}")]
        public async Task<IActionResult> GetListingCommentsAsync(string listingId, int count, int offset)
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
        [Route("getUserCommentsAsync/{userId}/{count:int}/{offset:int}")]
        public async Task<IActionResult> GetUserCommentsAsync(string userId, int count, int offset)
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
