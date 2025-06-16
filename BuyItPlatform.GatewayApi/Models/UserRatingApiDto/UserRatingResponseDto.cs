namespace BuyItPlatform.GatewayApi.Models.UserRatingApiDto
{
    public class UserRatingResponseDto
    {
        public string TargetUserId { get; set; } = "";
        public int AverageRating { get; set; } = 8;
        public int NumberOfRatings { get; set; }
    }
}
