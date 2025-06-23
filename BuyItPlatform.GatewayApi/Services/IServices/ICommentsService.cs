using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.CommentsApiDto;

namespace BuyItPlatform.GatewayApi.Services.IServices
{
    public interface ICommentsService
    {
        Task<MicroserviceResponseDto<object>> UploadCommentAsync(CommentViewDto commentDto);
        Task<MicroserviceResponseDto<object>> DeleteCommentAsync(string commentId);
        Task<MicroserviceResponseDto<object>> DeleteUserCommentsAsync(string userId);
        Task<MicroserviceResponseDto<object>> DeleteListingCommentsAsync(string listingId);
        Task<MicroserviceResponseDto<List<CommentViewDto>>> GetListingCommentsAsync(string listingId, int count, int offset);
        Task<MicroserviceResponseDto<List<CommentViewDto>>> GetUserCommentsAsync(string userId, int count, int offset);
    }
}
