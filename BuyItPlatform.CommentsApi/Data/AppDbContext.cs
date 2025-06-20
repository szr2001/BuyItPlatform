using BuyItPlatform.CommentsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BuyItPlatform.CommentsApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
