﻿using Aspnetcore.Fundamentals.Models;
using Aspnetcore.Fundamentals.Services;
using Aspnetcore.Fundamentals.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Aspnetcore.Fundamentals.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGreeter _greeter;
        private readonly IRestaurantData _restaurantData;

        public HomeController(IRestaurantData restaurantData, IGreeter greeter)
        {
            _restaurantData = restaurantData;
            _greeter = greeter;
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                Restaurants = _restaurantData.GetAll(),
                CurrentMessage = _greeter.GetGreeting()
            };

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = _restaurantData.Get(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // particularly a form POST, is very important when you are authenticating users using cookies.
        public IActionResult Create(RestaurantEditViewModel model)
        {
            if (!ModelState.IsValid) return View();

            var newRestaurant = new Restaurant
            {
                Cuisine = model.Cuisine,
                Name = model.Name
            };
            newRestaurant = _restaurantData.Add(newRestaurant);

            return RedirectToAction("Details", new {id = newRestaurant.Id});
        }
    }
}