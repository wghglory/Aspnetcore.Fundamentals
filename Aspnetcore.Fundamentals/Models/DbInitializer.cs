using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Aspnetcore.Fundamentals.Models
{
    public static class DbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            FoodDbContext context = applicationBuilder.ApplicationServices.GetRequiredService<FoodDbContext>();

            if (!context.Restaurants.Any())
            {
                context.Restaurants.AddRange(
                    new Restaurant {Name = "The House of Kobe", Cuisine = CuisineType.American},
                    new Restaurant {Name = "LJ's and the Kat", Cuisine = CuisineType.French},
                    new Restaurant {Name = "King's Contrivance", Cuisine = CuisineType.Italian}
                );
            }

            context.SaveChanges();
        }
    }
}