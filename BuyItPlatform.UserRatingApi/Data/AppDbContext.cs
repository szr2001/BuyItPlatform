using BuyItPlatform.UserRatingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BuyItPlatform.UserRatingApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserRating> Ratings { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
