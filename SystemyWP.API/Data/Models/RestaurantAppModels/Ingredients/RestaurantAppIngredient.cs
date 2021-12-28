using System.ComponentModel.DataAnnotations;
using SystemyWP.API.Data.Enums;
using SystemyWP.API.Data.Models.Abstractions;
using SystemyWP.API.Data.Models.UsersManagement.Access;

namespace SystemyWP.API.Data.Models.RestaurantAppModels.Ingredients
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

        [Required]
        public AccessKey AccessKey { get; set; }
        [Required]
        public int RestaurantAccessKeyId { get; set; }
    }
}