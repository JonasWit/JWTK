namespace SystemyWP.API.Data.DTOs.Gastronomy.Dishes;

public class DishUpdatePayload
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
}