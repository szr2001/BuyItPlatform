using AutoMapper;
using BuyItPlatform.AuthApi.Data;
using BuyItPlatform.AuthApi.Extensions;
using BuyItPlatform.AuthApi.Models;
using BuyItPlatform.AuthApi.Service;
using BuyItPlatform.AuthApi.Service.IService;
using BuyItPlatform.AuthApi.Services;
using BuyItPlatform.AuthApi.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;

namespace BuyItPlatform.AuthApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
            builder.Services.AddSingleton(mapper);

            builder.Services.AddIdentity<BuyItUser, BuyItRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped<ITokenCookiesProvider, TokensProvider>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IJwtTokenHandler, JwtTokenHandler>();
            builder.Services.AddScoped<IImageUploader, ImageUploaderMockService>();
            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllers();
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

                //test for mocking user profile images
                //in production we will use something like AWS blob storage
                //but until the we save them in the UserPicsMock
                //save the url in the user db, and display the img from the frontend
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(
                    Path.Combine(builder.Environment.ContentRootPath, "UserPicsMock")),
                    RequestPath = "/UserPicsMock"
                });
            }
            app.UseHttpsRedirection();

            //specify the app should use the authentication and authorization we added
            //in the dependency injection
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            ApplyMigration();

            app.Run();

            void ApplyMigration()
            {
                using (var scope = app.Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    if (db.Database.GetMigrations().Count() > 0)
                    {
                        db.Database.Migrate();
                    }
                }
            }
        }
    }
}
