namespace BuyItPlatform.AuthApi.Models.Dto
{
    public class UserProfileDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; } = -1;
        public string ProfileImgLink { get; set; }
        public string PhoneNumber { get; set; }
    }
}
