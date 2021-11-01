using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.RestaurantAppModels.Access;
using SystemyWP.Data.Models.RestaurantAppModels.Ingredients;
using SystemyWP.Data.Models.RestaurantAppModels.Menus;

namespace SystemyWP.Data.Models.RestaurantAppModels.Dishes
{
    public class RestaurantAppDish : TrackedModel<long>
    {
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        
        public List<RestaurantAppIngredient> RestaurantAppIngredients { get; set; } = new();
        public List<RestaurantAppMenu> RestaurantAppMenus { get; set; } = new();       
        
        [Required]
        public RestaurantAccessKey RestaurantAccessKey { get; set; }
        [Required]
        public int RestaurantAccessKeyId { get; set; }
    }
}