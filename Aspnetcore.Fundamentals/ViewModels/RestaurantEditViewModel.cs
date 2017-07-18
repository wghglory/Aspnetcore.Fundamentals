using System.ComponentModel.DataAnnotations;
using Aspnetcore.Fundamentals.Models;

namespace Aspnetcore.Fundamentals.ViewModels
{
    public class RestaurantEditViewModel
    {
        [Required, MaxLength(80)]
        public string Name { get; set; }

        public CuisineType Cuisine { get; set; }
    }
}