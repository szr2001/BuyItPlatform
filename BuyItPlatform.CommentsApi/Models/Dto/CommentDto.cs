using System.ComponentModel.DataAnnotations;

namespace BuyItPlatform.CommentsApi.Models.Dto
{
    public class CommentDto
    {
        public string Content { get; set; }
        public string ListingId { get; set; }
        public string? UserId { get; set; }
    }
}
