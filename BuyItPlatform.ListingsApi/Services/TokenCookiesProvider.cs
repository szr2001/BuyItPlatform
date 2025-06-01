using BuyItPlatform.ListingsApi.Services.IServices;

namespace BuyItPlatform.ListingsApi.Services
{
    public class TokensProvider : ITokenCookiesProvider
    {
        private IHttpContextAccessor contextAccessor;

        public TokensProvider(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        public string? GetToken()
        {
            string? token = null;
            bool? hasToken = contextAccessor.HttpContext?.Request.Cookies.TryGetValue(ITokenCookiesProvider.TokenKey, out token);
            return hasToken is true ? token : null;
        }

        public string? GetRefreshToken()
        {
            string? refreshToken = null;
            bool? hasToken = contextAccessor.HttpContext?.Request.Cookies.TryGetValue(ITokenCookiesProvider.RefreshTokenKey, out refreshToken);
            return hasToken is true ? refreshToken : null;
        }
    }
}
