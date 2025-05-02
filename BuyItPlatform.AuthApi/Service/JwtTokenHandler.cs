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

        public JwtTokenHandler(IOptions<JwtOptions> jwtOptions)
        {
            this.jwtOptions = jwtOptions.Value;
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

        public ClaimsPrincipal? GetTokenPrincipal(string token)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Secret));
            var validation = new TokenValidationParameters
            {
                IssuerSigningKey = securityKey,
                ValidateLifetime = false,
                ValidateActor = false,
                ValidateAudience = false,
                ValidateIssuer = false,
            };

            return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _ );
        }

        public string GenerateToken(BuyItUser user, IEnumerable<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            //encode the key
            var key = Encoding.ASCII.GetBytes(jwtOptions.Secret);

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
                Expires = DateTime.UtcNow.AddSeconds(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            //create token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //return the encrypted token as string
            return tokenHandler.WriteToken(token);
        }
    }
}
