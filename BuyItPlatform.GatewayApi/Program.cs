
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
                                    .AllowAnyHeader());
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseCors("AllowReactApp");
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
