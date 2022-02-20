using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.API.Gastronomy.Data.Enums;

namespace SystemyWP.API.Gastronomy.Data.Models
{
    public class Ingredient : BaseModel
    {
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }= "";
        
        [MaxLength(1000)]
        public string Description { get; set; }= "";
        
        public MeasurementUnits MeasurementUnits { get; set; }

        public float PricePerStack { get; set; }
        public float StackSize { get; set; }
        
        public List<Dish> RestaurantAppDishes { get; set; } = new();
    }
}