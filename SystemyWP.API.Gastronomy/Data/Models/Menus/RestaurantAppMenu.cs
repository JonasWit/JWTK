using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.API.Gastronomy.Data.Models.BaseClasses;
using SystemyWP.API.Gastronomy.Data.Models.Dishes;

namespace SystemyWP.API.Gastronomy.Data.Models.Menus
{
    public class RestaurantAppMenu : BaseModel
    {
        [Required]
        [MaxLength(500)]
        public string Name { get; set; } = "";
        
        [MaxLength(1000)]
        public string Description { get; set; }= "";
        
        public List<RestaurantAppDish> RestaurantAppDishes { get; set; } = new();
    }
}