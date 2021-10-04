using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.RestaurantAppModels.Access;

namespace SystemyWP.Data.Models.RestaurantAppModels.Menus
{
    public class RestaurantAppMenu : TrackedModel<long>
    {
        [Required]
        public RestaurantAccessKey RestaurantAccessKey { get; set; }
        [Required]
        public int RestaurantAccessKeyId { get; set; }
    }
}