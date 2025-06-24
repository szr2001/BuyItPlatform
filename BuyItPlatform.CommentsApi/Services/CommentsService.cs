using AutoMapper;
using BuyItPlatform.CommentsApi.Data;
using BuyItPlatform.CommentsApi.Models;
using BuyItPlatform.CommentsApi.Models.Dto;
using BuyItPlatform.CommentsApi.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace BuyItPlatform.CommentsApi.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;
        public CommentsService(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<int> CountListingCommentsAsync(string listingId)
        {
            return await dbContext.Comments.Where(c => c.ListingId == listingId).CountAsync();
        }

        public async Task DeleteCommentAsync(string commentId, string userId)
        {
            var comment = await dbContext.Comments.FindAsync(Guid.Parse(commentId));
            if (comment == null)
            {
                throw new KeyNotFoundException("commentId does't exist");
            }
            if(comment.UserId != userId)
            {
                throw new UnauthorizedAccessException("You don't own that comment");
            }
            dbContext.Comments.Remove(comment);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteListingCommentsAsync(string listingId)
        {
            var comment = await dbContext.Comments.Where(c => c.ListingId == listingId).ToListAsync();
            if (comment == null)
            {
                throw new KeyNotFoundException("listingId does't exist");
            }
            dbContext.Comments.RemoveRange(comment);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserCommentsAsync(string userId)
        {
            var comment = await dbContext.Comments.Where(c => c.UserId == userId).ToListAsync();
            if (comment != null)
            {
                dbContext.Comments.RemoveRange(comment);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Comment>> GetListingCommentsAsync(string listingId, int count, int offset)
        {
            return await dbContext.Comments.Where(c => c.ListingId == listingId).Skip(offset).Take(count).ToListAsync();
        }
        public async Task<ICollection<Comment>> GetUserCommentsAsync(string userId, int count, int offset)
        {
            return await dbContext.Comments.Where(c => c.UserId == userId).Skip(offset).Take(count).ToListAsync();
        }

        public async Task<Guid> UploadCommentAsync(CommentDto commentDto)
        {
            if (string.IsNullOrEmpty(commentDto.Content) || commentDto.Content?.Length > 200)
            {
                throw new ArgumentException("Comment must be between 1-200 characters M'lord!");
            }

            var comment = mapper.Map<Comment>(commentDto);
            dbContext.Comments.Add(comment);
            await dbContext.SaveChangesAsync();
            return comment.Id;
        }
    }
}
