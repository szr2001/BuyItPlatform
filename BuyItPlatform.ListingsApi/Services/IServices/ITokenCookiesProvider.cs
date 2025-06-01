namespace BuyItPlatform.ListingsApi.Services.IServices
{
    public interface ITokenCookiesProvider
    {
        public const string TokenKey = "token";
        public const string RefreshTokenKey = "refreshToken";
        string? GetRefreshToken();
        string? GetToken();
    }
}
