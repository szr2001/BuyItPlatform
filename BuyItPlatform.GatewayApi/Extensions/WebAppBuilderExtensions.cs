using BuyItPlatform.GatewayApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BuyItPlatform.GatewayApi.Extensions
{
    //extension class to hold the method for adding the JWT authentication logic in the DI 
    //instead of writing it in the program.cs because it looks cleaner with a extension class
    public static class WebAppBuilderExtensions
    {
        public static WebApplicationBuilder AddJwtAuthentication(this WebApplicationBuilder builder)
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
                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        //Because the frontend sends the token in the http only cookie
                        //and not in the Authorize header, we need to intercept
                        //the request, extract the token from the cookies manually
                        //and assign it as the recieved token for further verifications
                        //then further in microservice calls we correctly assign it to the Authorization headers
                        //so we only need to do this in the gateway because we can't assign the token
                        //from the frontend because it's a http only cookie and not accesable with js
                        var accessToken = context.Request.Cookies["token"];
                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
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
