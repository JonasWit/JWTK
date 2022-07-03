using FluentValidation;
using SystemyWP.API.Gastronomy.Data.DTOs.MenuDTOs;

namespace SystemyWP.API.Gastronomy.FluentValidations.Menu;

public class MenuBasicDtoValidation : AbstractValidator<MenuBasicDto>
{
    public MenuBasicDtoValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.AccessKey).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(AppConstants.DataLimits.NameLimit);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(AppConstants.DataLimits.DescriptionLimit);
        RuleFor(x => x.Category).MaximumLength(AppConstants.DataLimits.NameLimit);
    }
}