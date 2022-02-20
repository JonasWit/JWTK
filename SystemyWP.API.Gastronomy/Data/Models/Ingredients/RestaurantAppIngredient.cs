using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.API.Gastronomy.Data.Enums;
using SystemyWP.API.Gastronomy.Data.Models.BaseClasses;
using SystemyWP.API.Gastronomy.Data.Models.Dishes;

namespace SystemyWP.API.Gastronomy.Data.Models.Ingredients
{
    public class RestaurantAppIngredient : BaseModel
    {
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }= "";
        public MeasurementUnits MeasurementUnits { get; set; }

        public float PricePerStack { get; set; }
        public float StackSize { get; set; }
        
        public List<RestaurantAppDish> RestaurantAppDishes { get; set; } = new();   

        [Required]
        public string AccessKey { get; set; }= "";
    }
}