using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Enums;
using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.RestaurantAppModels.Access;

namespace SystemyWP.Data.Models.RestaurantAppModels.Ingredients
{
    public class RestaurantAppIngredient : TrackedModel
    {
        [Key]
        [Required]
        public long Id { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        public MeasurementUnits MeasurementUnits { get; set; }

        public float PricePerStack { get; set; }
        public float StackSize { get; set; }

        public List<RestaurantAppUsedIngredient> RestaurantAppUsedIngredients { get; set; }
        
        [Required]
        public RestaurantAccessKey RestaurantAccessKey { get; set; }
        [Required]
        public int RestaurantAccessKeyId { get; set; }
    }
}