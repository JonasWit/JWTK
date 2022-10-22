namespace MasterService.API.Data.DTOs.Gastronomy.Menus;

public class MenuDishUpdateDto
{
    public string AccessKey { get; set; }
    public long DishId { get; set; }
    public long MenuId { get; set; }
}