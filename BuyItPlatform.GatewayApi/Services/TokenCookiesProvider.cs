﻿using BuyItPlatform.GatewayApi.Services.IServices;
using Newtonsoft.Json.Linq;
using System.Reflection.Metadata.Ecma335;

namespace BuyItPlatform.GatewayApi.Services
{
    public class TokenCookiesProvider : ITokenCookiesProvider
    {
        private IHttpContextAccessor contextAccessor;

        public TokenCookiesProvider(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        public void ClearTokens()
        {
            var tokenOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(-1)
            };

            contextAccessor.HttpContext?.Response.Cookies.Append(ITokenCookiesProvider.TokenKey, "", tokenOptions);
            contextAccessor.HttpContext?.Response.Cookies.Append(ITokenCookiesProvider.RefreshTokenKey, "", tokenOptions);
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

        public void SetToken(string token)
        {
            var tokenOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(1)
            };
            contextAccessor.HttpContext?.Response.Cookies.Append(ITokenCookiesProvider.TokenKey, token, tokenOptions);
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
            contextAccessor.HttpContext?.Response.Cookies.Append(ITokenCookiesProvider.RefreshTokenKey, refreshToken, tokenOptions);
        }
    }
}
