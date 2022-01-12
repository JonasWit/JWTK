using System.ComponentModel.DataAnnotations;
using SystemyWP.API.Gastronomy.Data.Models.Abstractions;
using SystemyWP.API.Gastronomy.Data.Models.Dishes;

namespace SystemyWP.API.Gastronomy.Data.Models.Menus
{
    public class RestaurantAppMenu : TrackedModel
    {
        [Key]
        [Required]
        public long Id { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string Name { get; set; } = "";
        
        [MaxLength(1000)]
        public string Description { get; set; }= "";
        
        public List<RestaurantAppDish> RestaurantAppDishes { get; set; } = new();
        
        [Required]
        public string AccessKey { get; set; }= "";
    }
}