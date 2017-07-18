using System.Collections.Generic;
using Aspnetcore.Fundamentals.Models;

namespace Aspnetcore.Fundamentals.ViewModels
{
    public class HomeViewModel
    {
        public string CurrentMessage { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }
    }
}