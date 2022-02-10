using System.ComponentModel.DataAnnotations;
using SystemyWP.API.Gastronomy.Data.Models.Abstractions;
using SystemyWP.API.Gastronomy.Data.Models.Dishes;

namespace SystemyWP.API.Gastronomy.Data.Models.Ingredients
{
    public class RestaurantAppUsedIngredient : TrackedModel
    {
        [Key]
        [Required]
        public long Id { get; set; }
        
        public float UsedAmount { get; set; }
        
        public RestaurantAppDish RestaurantAppDish { get; set; }
        public long RestaurantAppDishId { get; set; }

        public RestaurantAppIngredient RestaurantAppIngredient { get; set; }
        public long RestaurantAppIngredientId { get; set; }
        
        [Required]
        public string AccessKey { get; set; }= "";
    }
}