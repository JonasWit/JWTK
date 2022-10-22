using System.Collections.Generic;

namespace MasterService.API.Data.DTOs.Gastronomy.Dishes;

public class DishDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Image { get; set; }
    public List<string> Ingredients { get; set; } = new();
    public List<string> Menus { get; set; } = new();
}