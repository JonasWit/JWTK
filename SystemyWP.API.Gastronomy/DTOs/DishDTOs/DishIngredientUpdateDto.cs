namespace SystemyWP.API.Gastronomy.DTOs.DishDTOs;

public class DishIngredientUpdateDto
{
    public string AccessKey { get; set; }
    public long DishId { get; set; }
    public long IngredientId { get; set; }
}