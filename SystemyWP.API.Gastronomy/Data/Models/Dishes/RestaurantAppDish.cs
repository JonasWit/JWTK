using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.API.Gastronomy.Data.Models.Abstractions;
using SystemyWP.API.Gastronomy.Data.Models.Ingredients;
using SystemyWP.API.Gastronomy.Data.Models.Menus;

namespace SystemyWP.API.Gastronomy.Data.Models.Dishes
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
    }
}