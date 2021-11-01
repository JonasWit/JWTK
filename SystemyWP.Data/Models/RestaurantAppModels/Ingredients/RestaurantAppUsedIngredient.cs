using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.RestaurantAppModels.Dishes;

namespace SystemyWP.Data.Models.RestaurantAppModels.Ingredients
{
    public class RestaurantAppUsedIngredient : TrackedModel<long>
    {
        public float UsedAmount { get; set; }
        
        [Required]
        public RestaurantAppDish RestaurantAppDish { get; set; }
        [Required]
        public long RestaurantAppDishId { get; set; }        
        
        [Required]
        public RestaurantAppIngredient RestaurantAppIngredient { get; set; }
        [Required]
        public long RestaurantAppIngredientId { get; set; }
    }
}