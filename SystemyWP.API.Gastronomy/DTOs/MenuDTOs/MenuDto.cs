using System.Collections.Generic;
using SystemyWP.API.Gastronomy.Data.Models;

namespace SystemyWP.API.Gastronomy.DTOs.MenuDTOs;

public class MenuDto : MenuBasicDto
{
    public List<Dish> Dishes { get; set; } = new();
}