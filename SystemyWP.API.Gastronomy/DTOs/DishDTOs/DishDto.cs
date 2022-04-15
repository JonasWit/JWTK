using System.Collections.Generic;

namespace SystemyWP.API.Gastronomy.DTOs.DishDTOs;

public class DishDto : DishBasicDto
{
    public List<long> Ingredients { get; set; } = new();

    public List<long> Menus { get; set; } = new();
}