using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.API.Data.Models.Abstractions;
using SystemyWP.API.Data.Models.RestaurantAppModels.Ingredients;
using SystemyWP.API.Data.Models.RestaurantAppModels.Menus;

namespace SystemyWP.API.Data.Models.RestaurantAppModels.Dishes
{
    public class RestaurantAppDish : TrackedModel
    {
        [Key]
        [Required]
        public long Id { get; set; }          
        
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        
        public List<RestaurantAppUsedIngredient> RestaurantAppUsedIngredients { get; set; } = new();
        public List<RestaurantAppMenu> RestaurantAppMenus { get; set; } = new();       
        
        [Required]
        public string AccessKey { get; set; }
        [Required]
        public int AccessKeyId { get; set; }
    }
}