namespace SystemyWP.API.Gastronomy.Data.DTOs.MenuDTOs;

public class MenuCreateDto
{
    public string AccessKey { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Category { get; set; }
    public string Image { get; set; }
}