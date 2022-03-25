using SystemyWP.Lib.Shared.DTOs.BaseDtos;

namespace SystemyWP.Lib.Shared.DTOs.Gastronomy;

public class DishCreateDto : BasicDto
{
    public string Name { get; set; } = "";

    public string Description { get; set; } = "";
}