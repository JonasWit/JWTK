namespace MasterService.API.Data.DTOs.Gastronomy.Menus;

public class MenuCreateDto
{
    public string AccessKey { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Image { get; set; }
}