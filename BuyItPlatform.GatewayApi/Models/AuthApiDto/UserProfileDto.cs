using BuyItPlatform.GatewayApi.Models.ListingApiDto;

namespace BuyItPlatform.GatewayApi.Models.AuthApiDto
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
        public ICollection<ListingViewDto> Listings { get; set; }
    }
}
