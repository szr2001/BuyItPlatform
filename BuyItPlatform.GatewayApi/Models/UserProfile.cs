namespace BuyItPlatform.GatewayApi.Models.Dto
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public string ProfileImgLink { get; set; }
        public string PhoneNumber { get; set; }
    }
}
