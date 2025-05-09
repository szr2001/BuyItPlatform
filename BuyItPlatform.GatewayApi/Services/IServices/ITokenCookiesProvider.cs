namespace BuyItPlatform.GatewayApi.Services.IServices
{
    public interface ITokenCookiesProvider
    {
        public const string TokenKey = "token";
        public const string RefreshTokenKey = "refreshToken";
        void ClearTokens();
        string? GetRefreshToken();
        string? GetToken();
        void SetRefreshToken(string refreshToken);
        void SetToken(string token);
    }
}
