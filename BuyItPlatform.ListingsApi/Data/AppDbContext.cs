using BuyItPlatform.ListingsApi.Enums;
using BuyItPlatform.ListingsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BuyItPlatform.ListingsApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Listing> Listings { get; set; }
        public DbSet<ListingImage> ListingImages { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ListingImage>()
                .HasOne(i => i.ParentListing)
                .WithMany(p => p.Images)
                .HasForeignKey(i => i.ListingId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Listing>().HasData(new Listing
            {
                Id = 1,
                Name = "Chair",
                Description = "A good chair",
                Category = Category.HomeGarden,
                Tags = [Tag.Wood, Tag.Compact],
                Color = Color.Brown,
                Price = 10,
                Currency = "Euro",
                UserId = 123,
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
