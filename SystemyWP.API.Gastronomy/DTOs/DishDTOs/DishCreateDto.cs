namespace SystemyWP.API.Gastronomy.DTOs.DishDTOs;

public class DishCreateDto : BasicDto
{
    public string Name { get; set; } = "";

    public string Description { get; set; } = "";
}