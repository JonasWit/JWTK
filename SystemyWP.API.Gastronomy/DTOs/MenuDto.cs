using System.Collections.Generic;

namespace SystemyWP.API.Gastronomy.DTOs;

public class MenuDto
{
    public string Name { get; set; } = "";

    public List<DishDto> RestaurantAppDishes { get; set; } = new();
}