using FluentValidation;
using MasterService.API.Gastronomy.Data.DTOs.MenuDTOs;

namespace MasterService.API.Gastronomy.FluentValidations.Menu;

public class MenuUpdateDishValidation: AbstractValidator<MenuDishUpdateDto>
{
    public MenuUpdateDishValidation()
    {
        RuleFor(x => x.AccessKey).NotEmpty();
        RuleFor(x => x.DishId).NotEmpty();
        RuleFor(x => x.MenuId).NotEmpty();
    }
}