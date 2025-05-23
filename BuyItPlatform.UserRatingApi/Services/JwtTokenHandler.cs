using BuyItPlatform.UserRatingApi.Models;
using BuyItPlatform.UserRatingApi.Service.IService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BuyItPlatform.UserRatingApi.Service
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
