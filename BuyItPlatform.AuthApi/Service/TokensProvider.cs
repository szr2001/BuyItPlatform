using BuyItPlatform.AuthApi.Services.IServices;

namespace BuyItPlatform.AuthApi.Services
{
    public class TokensProvider : ITokensProvider
    {
        private IHttpContextAccessor contextAccessor;

        public TokensProvider(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        public void ClearTokens()
        {
            contextAccessor.HttpContext?.Response.Cookies.Delete(ITokensProvider.TokenKey);
            contextAccessor.HttpContext?.Response.Cookies.Delete(ITokensProvider.RefreshTokenKey);
        }

        public string? GetToken()
        {
            string? token = null;
            bool? hasToken = contextAccessor.HttpContext?.Request.Cookies.TryGetValue(ITokensProvider.TokenKey, out token);
            return hasToken is true ? token : null;
        }

        public string? GetRefreshToken()
        {
            string? refreshToken = null;
            bool? hasToken = contextAccessor.HttpContext?.Request.Cookies.TryGetValue(ITokensProvider.RefreshTokenKey, out refreshToken);
            return hasToken is true ? refreshToken : null;
        }

        public void SetToken(string token)
        {
            var tokenOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(1)
            };
            contextAccessor.HttpContext?.Response.Cookies.Append(ITokensProvider.TokenKey, token, tokenOptions);
        }

        public void SetRefreshToken(string refreshToken)
        {
            var tokenOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            contextAccessor.HttpContext?.Response.Cookies.Append(ITokensProvider.RefreshTokenKey, refreshToken, tokenOptions);
        }
    }
}
