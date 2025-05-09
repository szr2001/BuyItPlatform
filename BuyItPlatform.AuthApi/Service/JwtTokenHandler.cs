using BuyItPlatform.AuthApi.Models;
using BuyItPlatform.AuthApi.Models.Dto;
using BuyItPlatform.AuthApi.Service.IService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BuyItPlatform.AuthApi.Service
{
    public class JwtTokenHandler : IJwtTokenHandler
    {
        private readonly JwtOptions jwtOptions;
        private readonly JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        private readonly ICollection<Claim> claims = new List<Claim>();
        private readonly byte[] secretKey;
        public JwtTokenHandler(IOptions<JwtOptions> jwtOptions)
        {
            this.jwtOptions = jwtOptions.Value;

            //read the secret code and encode it
            secretKey = Encoding.ASCII.GetBytes(jwtOptions.Value.Secret);
        }

        public ICollection<Claim> ExtractTokenData(string token)
        {
            //extract the claims inside the token, without validaing it because it was
            //already validated in the authorization layer
            //we can't get to the point of calling this if the token is not valid
            var principal = tokenHandler.ReadJwtToken(token);

            return principal.Claims.ToList();
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];

            using(var numberGenerator = RandomNumberGenerator.Create())
            {
                numberGenerator.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);
        }

        public string GenerateToken(BuyItUser user, IEnumerable<string> roles)
        {
            //create the cookies data, add more based on the requirements
            var claims = new List<Claim> 
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            //set the token data
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = jwtOptions.Audiance,
                Issuer = jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            //create token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //return the encrypted token as string
            return tokenHandler.WriteToken(token);
        }
    }
}
