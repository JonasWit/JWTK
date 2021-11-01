using SystemyWP.Data.Enums;
using SystemyWP.Data.Models.Abstractions;

namespace SystemyWP.Data.Models.RestaurantAppModels.Ingredients
{
    public class RestaurantAppIngredientStack : TrackedModel<long>
    {
        public double StackSize { get; set; }
        public double PricePerStack { get; set; }

        public long RestaurantAppIngredientId { get; set; }
        public RestaurantAppIngredient RestaurantAppIngredient { get; set; }
    }
}