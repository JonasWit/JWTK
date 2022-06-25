using FluentValidation;
using SystemyWP.API.Data.DTOs.Gastronomy.Menus;

namespace SystemyWP.API.FluentValidations.GastronomyService.Menus;

public class MenuDishUpdateValidation : AbstractValidator<MenuDishUpdatePayload>
{
    public MenuDishUpdateValidation()
    {
        RuleFor(x => x.DishId).NotEmpty();
        RuleFor(x => x.MenuId).NotEmpty();
    }
}