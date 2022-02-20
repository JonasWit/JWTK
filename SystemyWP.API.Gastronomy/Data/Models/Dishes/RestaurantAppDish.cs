using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.API.Gastronomy.Data.Models.BaseClasses;
using SystemyWP.API.Gastronomy.Data.Models.Ingredients;
using SystemyWP.API.Gastronomy.Data.Models.Menus;

namespace SystemyWP.API.Gastronomy.Data.Models.Dishes
{
    public class RestaurantAppDish : BaseModel
    {
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        
        public List<RestaurantAppIngredient> RestaurantAppIngredients { get; set; } = new();
        
        public List<RestaurantAppMenu> RestaurantAppMenus { get; set; } = new();       
        
        [Required]
        public string AccessKey { get; set; }
    }
}