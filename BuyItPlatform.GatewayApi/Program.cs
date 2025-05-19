
using BuyItPlatform.GatewayApi.Extensions;
using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Service;
using BuyItPlatform.GatewayApi.Service.IService;
using BuyItPlatform.GatewayApi.Services;
using BuyItPlatform.GatewayApi.Services.IServices;
using BuyItPlatform.GatewayApi.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace BuyItPlatform.GatewayApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //temporary allow Cross-Origin Resource Sharing for testing
            //allowing the frontend on localhost and the port to access the api on a different port
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp",
                    policy => policy.WithOrigins("http://localhost:52633")
                                    .AllowAnyMethod()
                                    .AllowCredentials()
                                    .AllowAnyHeader());
            });
            builder.Services.AddControllers();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient();

            builder.Services.AddSingleton<MicroservicesUrls>();
            builder.Services.AddScoped<ITokenCookiesProvider, TokenCookiesProvider>();
            builder.Services.AddScoped<IApiCallsService, ApiCallsService>();
            builder.Services.AddScoped<IListingsService, ListingsService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserRatingService, UserRatingService>();
            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                //add the UI for the token input
                option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: 'Bearer Generated-JWT-Token'",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme,

                            }
                        },new string[]{ }
                    }
                });
            });

            //adds the custom authentication using JWT
            builder.AddJwtAuthentication();

            //adds the default authorization
            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
                app.UseCors("AllowReactApp");

            app.UseHttpsRedirection();

            //specify the app should use the authentication and authorization we added
            //in the dependency injection
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
