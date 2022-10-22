namespace MasterService.API.Data.DTOs.Gastronomy.Dishes;

public class DishIngredientUpdateDto
{
    public string AccessKey { get; set; }
    public long DishId { get; set; }
    public long IngredientId { get; set; }
}