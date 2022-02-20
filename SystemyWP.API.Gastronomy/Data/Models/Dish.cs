using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SystemyWP.API.Gastronomy.Data.Models
{
    public class Dish : BaseModel
    {
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }= "";
        
        public List<Ingredient> RestaurantAppIngredients { get; set; } = new();
        
        public List<Menu> RestaurantAppMenus { get; set; } = new();
    }
}