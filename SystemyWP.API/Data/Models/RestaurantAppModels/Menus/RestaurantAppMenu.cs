using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.API.Data.Models.Abstractions;
using SystemyWP.API.Data.Models.RestaurantAppModels.Dishes;
using SystemyWP.API.Data.Models.UsersManagement.Access;

namespace SystemyWP.API.Data.Models.RestaurantAppModels.Menus
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
        public AccessKey AccessKey { get; set; }
        [Required]
        public int RestaurantAccessKeyId { get; set; }
    }
}