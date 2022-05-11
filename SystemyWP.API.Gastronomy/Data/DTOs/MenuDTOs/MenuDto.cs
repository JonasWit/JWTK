using System.Collections.Generic;

namespace SystemyWP.API.Gastronomy.Data.DTOs.MenuDTOs;

public class MenuDto : MenuBasicDto
{
    public List<long> Dishes { get; set; } = new();
}