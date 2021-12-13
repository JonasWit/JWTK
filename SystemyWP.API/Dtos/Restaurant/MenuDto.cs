using System.Collections.Generic;
using SystemyWP.Data.Models.RestaurantAppModels.Dishes;

namespace SystemyWP.API.Dtos.Restaurant;

public class MenuDto
{
    public string Name { get; set; }

    public List<RestaurantAppDishDto> RestaurantAppDishes { get; set; } = new();
}