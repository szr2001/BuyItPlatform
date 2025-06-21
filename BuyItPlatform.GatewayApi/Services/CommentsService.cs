using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.CommentsApiDto;
using BuyItPlatform.GatewayApi.Services.IServices;
using BuyItPlatform.GatewayApi.Utility;

namespace BuyItPlatform.GatewayApi.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly IApiCallsService apiCallsService;
        private readonly MicroservicesUrls microservicesUrl;

        public CommentsService(IApiCallsService apiCallsService, MicroservicesUrls microservicesUrl)
        {
            this.apiCallsService = apiCallsService;
            this.microservicesUrl = microservicesUrl;
        }

        public async Task<MicroserviceResponseDto<object>> DeleteCommentAsync(string commentId)
        {
            return await apiCallsService.SendAsync<object>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{microservicesUrl.CommentsApiUrl}/deleteComment/{commentId}",
            });
        }

        public async Task<MicroserviceResponseDto<object>> DeleteListingCommentsAsync(string listingId)
        {
            return await apiCallsService.SendAsync<object>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{microservicesUrl.CommentsApiUrl}/deleteListingComments/{listingId}",
            });
        }

        public async Task<MicroserviceResponseDto<object>> DeleteUserCommentsAsync(string userId)
        {
            return await apiCallsService.SendAsync<object>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{microservicesUrl.CommentsApiUrl}/deleteUserComments/{userId}",
            });
        }

        public async Task<MicroserviceResponseDto<List<CommentDto>>> GetListingCommentsAsync(string listingId)
        {
            return await apiCallsService.SendAsync<List<CommentDto>>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{microservicesUrl.CommentsApiUrl}/getListingComments/{listingId}",
            });
        }

        public async Task<MicroserviceResponseDto<List<CommentDto>>> GetUserCommentsAsync(string userId)
        {
            return await apiCallsService.SendAsync<List<CommentDto>>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{microservicesUrl.CommentsApiUrl}/getUserComments/{userId}",
            });
        }

        public async Task<MicroserviceResponseDto<object>> UploadCommentAsync(CommentDto commentDto)
        {
            return await apiCallsService.SendAsync<object>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{microservicesUrl.CommentsApiUrl}/UploadComment",
                BodyData = commentDto
            });
        }
    }
}
