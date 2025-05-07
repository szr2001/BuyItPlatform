using BuyItPlatform.AuthApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BuyItPlatform.AuthApi.Extensions
{
    public static class WebAppBuilderExtensions
    {
        public static WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder)
        {
            //get the JWt settings
            var jwtOptions = builder.Configuration.GetSection("ApiSettings:JwtOptions").Get<JwtOptions>();
            if (jwtOptions == null)
            {
                throw new ArgumentNullException();
            }

            //encrypt the secret as a key
            var key = Encoding.ASCII.GetBytes(jwtOptions.Secret);

            //adds authentication in the dependency settings with the settings
            //to specify that we want to verify the jwt token
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audiance,
                    ClockSkew = TimeSpan.Zero,
                };
            });
            return builder;
        }
    }
}
