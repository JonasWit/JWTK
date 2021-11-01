using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Enums;
using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.RestaurantAppModels.Access;
using SystemyWP.Data.Models.RestaurantAppModels.Dishes;

namespace SystemyWP.Data.Models.RestaurantAppModels.Ingredients
{
    public class RestaurantAppIngredient : TrackedModel<long>
    {
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        public double Quantity { get; set; }
        public MeasurementUnits MeasurementUnits { get; set; }
        
        public List<RestaurantAppDish> RestaurantAppDishes { get; set; } = new();
        
        public long RestaurantAppIngredientStackId { get; set; }
        public RestaurantAppIngredientStack RestaurantAppIngredientStack { get; set; }
        
        [Required]
        public RestaurantAccessKey RestaurantAccessKey { get; set; }
        [Required]
        public int RestaurantAccessKeyId { get; set; }
    }
}