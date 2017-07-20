using System.Collections.Generic;
using System.Linq;
using Aspnetcore.Fundamentals.Models;

namespace Aspnetcore.Fundamentals.Services
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
        Restaurant Get(int id);
        Restaurant Add(Restaurant newRestaurant);
        void Commit();
    }

    public class MySqlRestaurantData : IRestaurantData
    {
        private readonly FoodDbContext _context;

        public MySqlRestaurantData(FoodDbContext context)
        {
            _context = context;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            _context.Add(newRestaurant);
            return newRestaurant;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public Restaurant Get(int id)
        {
            return _context.Restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _context.Restaurants;
        }
    }


    public class InMemoryRestaurantData : IRestaurantData
    {
        static InMemoryRestaurantData()
        {
            Restaurants = new List<Restaurant>
            {
                new Restaurant {Id = 1, Name = "The House of Kobe", Cuisine = CuisineType.American},
                new Restaurant {Id = 2, Name = "LJ's and the Kat", Cuisine = CuisineType.French},
                new Restaurant {Id = 3, Name = "King's Contrivance", Cuisine = CuisineType.Italian}
            };
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return Restaurants;
        }

        public Restaurant Get(int id)
        {
            return Restaurants.FirstOrDefault(r => r.Id == id);
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            newRestaurant.Id = Restaurants.Max(r => r.Id) + 1;
            Restaurants.Add(newRestaurant);

            return newRestaurant;
        }

        public void Commit()
        {
        }

        private static readonly List<Restaurant> Restaurants;
    }
}