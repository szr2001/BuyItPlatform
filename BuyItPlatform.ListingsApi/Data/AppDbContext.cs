using BuyItPlatform.ListingsApi.Enums;
using BuyItPlatform.ListingsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BuyItPlatform.ListingsApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Listing> Listings { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
