namespace BuyItPlatform.GatewayApi.Services.IServices
{
    public interface ITokensProvider
    {
        public const string TokenKey = "token";
        public const string RefreshTokenKey = "refreshToken";
        void ClearTokens();
        string? GetRefreshToken();
        string? GetToken();
        void SetTokens(string token, string refreshToken);
    }
}
