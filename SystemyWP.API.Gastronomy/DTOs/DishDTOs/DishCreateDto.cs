namespace SystemyWP.API.Gastronomy.DTOs.DishDTOs;

public class DishCreateDto
{
    public string AccessKey { get; set; }
    
    public string Type { get; set; }
    
    public string Name { get; set; } = "";

    public string Description { get; set; } = "";
}