using FluentValidation;
using MasterService.API.Constants;
using MasterService.API.Data.DTOs.Gastronomy.Menus;

namespace MasterService.API.FluentValidations.GastronomyService.Menus;

public class MenuCreateValidation : AbstractValidator<MenuCreatePayload>
{
    public MenuCreateValidation()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(AppConstants.DataLimits.NameLimit);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(AppConstants.DataLimits.DescriptionLimit);
        RuleFor(x => x.Category).MaximumLength(AppConstants.DataLimits.NameLimit);
    }
}