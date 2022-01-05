using System.ComponentModel.DataAnnotations;
using SystemyWP.API.Gastronomy.Data.Enums;
using SystemyWP.API.Gastronomy.Data.Models.Abstractions;

namespace SystemyWP.API.Gastronomy.Data.Models.Ingredients
{
    public class RestaurantAppIngredient : TrackedModel
    {
        [Key]
        [Required]
        public long Id { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        public MeasurementUnits MeasurementUnits { get; set; }

        public float PricePerStack { get; set; }
        public float StackSize { get; set; }

        [Required]
        public string AccessKey { get; set; }
    }
}