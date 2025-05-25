namespace BuyItPlatform.GatewayApi.Models.Dto
{
    public class UserRatingResponseDto
    {
        public string TargetUserId { get; set; } = "";
        public int AverageRating { get; set; } = 8;
        public int NumberOfRatings { get; set; }
    }
}
