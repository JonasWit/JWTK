using System.ComponentModel.DataAnnotations;
using SystemyWP.API.Data.Models.Abstractions;
using SystemyWP.API.Data.Models.RestaurantAppModels.Dishes;
using SystemyWP.API.Data.Models.UsersManagement.Access;

namespace SystemyWP.API.Data.Models.RestaurantAppModels.Ingredients
{
    public class RestaurantAppUsedIngredient : TrackedModel
    {
        [Key]
        [Required]
        public long Id { get; set; }
        
        public float UsedAmount { get; set; }
        
        [Required]
        public RestaurantAppDish RestaurantAppDish { get; set; }
        [Required]
        public long RestaurantAppDishId { get; set; }        
        
        [Required]
        public RestaurantAppIngredient RestaurantAppIngredient { get; set; }
        [Required]
        public long RestaurantAppIngredientId { get; set; }
        
        [Required]
        public AccessKey AccessKey { get; set; }
        [Required]
        public int RestaurantAccessKeyId { get; set; }
    }
}