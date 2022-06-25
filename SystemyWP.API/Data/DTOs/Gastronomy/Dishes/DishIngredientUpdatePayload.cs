namespace SystemyWP.API.Data.DTOs.Gastronomy.Dishes;

public class DishIngredientUpdatePayload
{
    public string AccessKey { get; set; }
    public long DishId { get; set; }
    public long IngredientId { get; set; }
}