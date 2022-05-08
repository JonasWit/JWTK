using FluentValidation;
using SystemyWP.API.Constants;
using SystemyWP.API.Data.DTOs.Gastronomy.Dishes;
using SystemyWP.API.Data.DTOs.Gastronomy.Menus;

namespace SystemyWP.API.FluentValidations.GastronomyService.Menus;

public class MenuUpdateValidation : AbstractValidator<MenuUpdateDto>
{
    public MenuUpdateValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(AppConstants.DataLimits.NameLimit);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(AppConstants.DataLimits.DescriptionLimit);
    }
}