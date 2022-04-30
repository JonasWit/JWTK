using System.Collections.Generic;

namespace SystemyWP.API.Data.DTOs.Gastronomy.Dishes;

public class DishDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> Ingredients { get; set; } = new();
    public List<string> Menus { get; set; } = new();
}