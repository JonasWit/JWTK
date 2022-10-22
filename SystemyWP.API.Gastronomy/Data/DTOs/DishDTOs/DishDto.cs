using System.Collections.Generic;

namespace MasterService.API.Gastronomy.Data.DTOs.DishDTOs;

public class DishDto : DishBasicDto
{
    public List<long> Ingredients { get; set; } = new();

    public List<long> Menus { get; set; } = new();
}