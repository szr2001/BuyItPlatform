namespace BuyItPlatform.GatewayApi.Models
{
    public class JwtOptions
    {
        public string Secret { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audiance { get; set; } = string.Empty;
    }
}
