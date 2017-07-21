using System.ComponentModel.DataAnnotations;

namespace Aspnetcore.Fundamentals.Models
{
    public enum CuisineType
    {
        None,
        Italian,
        French,
        Japanese,
        American
    }

    public class Restaurant
    {
        //By convention, a property named Id or <type name>Id will be configured as the key of an entity.
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)] or [Key] is not needed following this convention

        public int Id { get; set; }

        [Required, MaxLength(80)]
        [Display(Name = "Restaurant Name")]
        public string Name { get; set; }

        public CuisineType Cuisine { get; set; }
    }
}