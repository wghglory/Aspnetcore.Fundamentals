using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aspnetcore.Fundamentals.Models
{
    public class FoodDbContext : IdentityDbContext<User>
    {
        public FoodDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}