using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.General;
using SystemyWP.Data.Models.RestaurantAppModels.Dishes;
using SystemyWP.Data.Models.RestaurantAppModels.Ingredients;
using SystemyWP.Data.Models.RestaurantAppModels.Menus;

namespace SystemyWP.Data.Models.RestaurantAppModels.Access
{
    public class RestaurantAccessKey : TrackedModel
    {
        [Key]
        [Required]
        public long Id { get; set; }  
        
        [Required] 
        [MaxLength(50)] 
        public string Name { get; set; }
        
        [Required] 
        public DateTime ExpireDate { get; set; }
        public List<User> Users { get; set; } = new();
        
        public List<RestaurantAppMenu> RestaurantAppMenus { get; set; } = new();
        public List<RestaurantAppDish> RestaurantAppDishes { get; set; } = new();
        public List<RestaurantAppIngredient> RestaurantAppIngredients { get; set; } = new();
    }
}