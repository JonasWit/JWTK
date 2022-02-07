namespace SystemyWP.API.Gastronomy.DTOs;

public class MenuDto
{
    public string Name { get; set; } = "";

    public List<RestaurantAppDishDto> RestaurantAppDishes { get; set; } = new();
}