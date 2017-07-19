using Microsoft.EntityFrameworkCore;

namespace Aspnetcore.Fundamentals.Models
{
    public class FoodDbContext : DbContext
    {
        public FoodDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}