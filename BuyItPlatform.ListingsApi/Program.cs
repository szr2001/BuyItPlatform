
using AutoMapper;
using BuyItPlatform.ListingsApi.Data;
using BuyItPlatform.ListingsApi.Services;
using BuyItPlatform.ListingsApi.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace BuyItPlatform.ListingsApi
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
            builder.Services.AddSingleton<IImageUploader, ListingImagesTestService>();
            builder.Services.AddScoped<IListingsService, ListingsService>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            ApplyMigration();

            app.Run();

            void ApplyMigration()
            {
                using (var scope = app.Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    if(db.Database.GetMigrations().Count() > 0)
                    {
                        db.Database.Migrate();
                    }
                }
            }
        }

    }
}
