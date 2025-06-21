using BuyItPlatform.CommentsApi.Models;
using BuyItPlatform.CommentsApi.Models.Dto;

namespace BuyItPlatform.CommentsApi.Services.IServices
{
    public interface ICommentsService
    {
        Task UploadCommentAsync(CommentDto commentDto);
        Task DeleteCommentAsync(string commentId);
        Task DeleteUserCommentsAsync(string userId);
        Task DeleteListingCommentsAsync(string listingId);
        Task<ICollection<Comment>> GetListingCommentsAsync(string listingId, int count, int offset);
        Task<ICollection<Comment>> GetUserCommentsAsync(string userId, int count, int offset);
    }
}
