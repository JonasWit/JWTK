using FluentValidation;
using MasterService.API.Data.DTOs.Gastronomy.Menus;

namespace MasterService.API.FluentValidations.GastronomyService.Menus;

public class MenuDishUpdateValidation : AbstractValidator<MenuDishUpdatePayload>
{
    public MenuDishUpdateValidation()
    {
        RuleFor(x => x.DishId).NotEmpty();
        RuleFor(x => x.MenuId).NotEmpty();
    }
}