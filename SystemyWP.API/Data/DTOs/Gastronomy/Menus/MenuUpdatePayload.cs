namespace SystemyWP.API.Data.DTOs.Gastronomy.Menus;

public class MenuUpdatePayload
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Image { get; set; }
}