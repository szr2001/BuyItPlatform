namespace BuyItPlatform.GatewayApi.Models.Dto
{
    public class UserProfileDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public int AverageRating { get; set; } = -1;
        public int NumberOfRatings { get; set; }
        public string ProfileImgLink { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<ListingDto> Listings { get; set; }
    }
}
