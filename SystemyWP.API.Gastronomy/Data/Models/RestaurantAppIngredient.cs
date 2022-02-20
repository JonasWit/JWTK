using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.API.Gastronomy.Data.Enums;
using SystemyWP.API.Gastronomy.Data.Models.BaseClasses;

namespace SystemyWP.API.Gastronomy.Data.Models
{
    public class RestaurantAppIngredient : BaseModel
    {
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }= "";
        
        [MaxLength(1000)]
        public string Description { get; set; }= "";
        
        public MeasurementUnits MeasurementUnits { get; set; }

        public float PricePerStack { get; set; }
        public float StackSize { get; set; }
        
        public List<RestaurantAppDish> RestaurantAppDishes { get; set; } = new();
    }
}