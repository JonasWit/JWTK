using System.Collections.Generic;

namespace SystemyWP.API.Dtos.RestaurantApp;

public class MenuDto
{
    public string Name { get; set; }

    public List<RestaurantAppDishDto> RestaurantAppDishes { get; set; } = new();
}