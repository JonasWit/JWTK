using System.Collections.Generic;
using SystemyWP.API.Gastronomy.Data.Models;

namespace SystemyWP.API.Gastronomy.Data.DTOs.MenuDTOs;

public class MenuDto : MenuBasicDto
{
    public List<long> Dishes { get; set; } = new();
}