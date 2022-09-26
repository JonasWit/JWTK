namespace SystemyWP.API.Gastronomy.Data.DTOs.DishDTOs;

public class DishCreateDto
{
    public string AccessKey { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string Category { get; set; }
}