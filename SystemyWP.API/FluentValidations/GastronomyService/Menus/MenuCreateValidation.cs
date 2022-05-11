using FluentValidation;
using SystemyWP.API.Constants;
using SystemyWP.API.Data.DTOs.Gastronomy.Menus;

namespace SystemyWP.API.FluentValidations.GastronomyService.Menus;

public class MenuCreateValidation: AbstractValidator<MenuCreateDto>
{
    public MenuCreateValidation()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(AppConstants.DataLimits.NameLimit);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(AppConstants.DataLimits.DescriptionLimit);
    }
}