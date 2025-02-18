using BuyItPlatform.AuthApi.Models;
using BuyItPlatform.AuthApi.Service.IService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BuyItPlatform.AuthApi.Service
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions jwtOptions;

        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
        {
            this.jwtOptions = jwtOptions.Value;
        }

        public string GenerateToken(BuyItUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            //encode the key
            var key = Encoding.ASCII.GetBytes(jwtOptions.Secret);

            //create the cookies data, add more based on the requirements
            var claims = new List<Claim> 
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
            };

            //set the token data
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = jwtOptions.Audiance,
                Issuer = jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            //create token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
