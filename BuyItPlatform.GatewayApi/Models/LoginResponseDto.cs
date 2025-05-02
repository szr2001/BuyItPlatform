namespace BuyItPlatform.GatewayApi.Models
{
    public class LoginResponseDto
    {
        public UserDto? User { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
