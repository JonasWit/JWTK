using System.Collections.Generic;

namespace SystemyWP.API.Data.DTOs.Gastronomy.Menus;

public class MenuDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> Dishes { get; set; } = new();
}