namespace BuyItPlatform.AuthApi.Services.IServices
{
    public interface ITokensProvider
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
