using BuyItPlatform.ListingsApi.Services.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BuyItPlatform.ListingsApi.Services
{
    public class JwtTokenHandler : IJwtTokenHandler
    {
        private readonly JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        public ICollection<Claim> ExtractTokenData(string token)
        {
            //extract the claims inside the token, without validaing it because it was
            //already validated in the authorization layer
            //we can't get to the point of calling this if the token is not valid
            var principal = tokenHandler.ReadJwtToken(token);

            return principal.Claims.ToList();
        }
    }
}
