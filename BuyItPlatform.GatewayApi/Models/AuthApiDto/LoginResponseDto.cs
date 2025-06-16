namespace BuyItPlatform.GatewayApi.Models.AuthApiDto
{
    public class LoginResponseDto
    {
        public UserDto? User { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
