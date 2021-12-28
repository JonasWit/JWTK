using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.RestaurantAppModels.Access;
using SystemyWP.Data.Models.RestaurantAppModels.Dishes;

namespace SystemyWP.Data.Models.RestaurantAppModels.Menus
{
    public class RestaurantAppMenu : TrackedModel
    {
        [Key]
        [Required]
        public long Id { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        public List<RestaurantAppDish> RestaurantAppDishes { get; set; } = new();
        
        [Required]
        public RestaurantAccessKey RestaurantAccessKey { get; set; }
        [Required]
        public int RestaurantAccessKeyId { get; set; }
    }
}