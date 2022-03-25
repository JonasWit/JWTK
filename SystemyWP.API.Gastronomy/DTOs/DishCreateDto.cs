namespace SystemyWP.API.Gastronomy.DTOs;

public class DishCreateDto : BasicDto
{
    public string Name { get; set; } = "";

    public string Description { get; set; } = "";
}