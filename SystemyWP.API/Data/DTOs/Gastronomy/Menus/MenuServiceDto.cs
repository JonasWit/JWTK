using System.Collections.Generic;

namespace SystemyWP.API.Data.DTOs.Gastronomy.Menus;

public class MenuServiceDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public List<long> Dishes { get; set; } = new();
}