using System.Collections.Generic;

namespace SystemyWP.API.Data.DTOs.Gastronomy.Dishes;

public class DishServiceDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Image { get; set; }
    public List<long> Ingredients { get; set; } = new();
    public List<long> Menus { get; set; } = new();
}