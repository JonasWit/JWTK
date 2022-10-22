using System.Collections.Generic;

namespace MasterService.API.Gastronomy.Data.DTOs.MenuDTOs;

public class MenuDto : MenuBasicDto
{
    public List<long> Dishes { get; set; } = new();
}