
namespace BuyItPlatform.GatewayApi.Models.CommentsApiDto
{
    public class CommentViewDto
    {
        public string Content { get; set; }
        public string ListingId { get; set; }
        public string? UserId { get; set; }
        public string userName { get; set; }
        public string userProfilePic { get; set; }
    }
}
