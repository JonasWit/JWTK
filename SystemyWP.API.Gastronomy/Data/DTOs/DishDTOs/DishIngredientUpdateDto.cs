namespace MasterService.API.Gastronomy.Data.DTOs.DishDTOs;

public class DishIngredientUpdateDto
{
    public string AccessKey { get; set; }
    public long DishId { get; set; }
    public long IngredientId { get; set; }
}