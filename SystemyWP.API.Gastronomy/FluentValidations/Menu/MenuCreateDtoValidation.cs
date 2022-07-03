using FluentValidation;
using SystemyWP.API.Gastronomy.Data.DTOs.MenuDTOs;

namespace SystemyWP.API.Gastronomy.FluentValidations.Menu;

public class MenuCreateDtoValidation : AbstractValidator<MenuCreateDto>
{
    public MenuCreateDtoValidation()
    {
        RuleFor(x => x.AccessKey).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(AppConstants.DataLimits.NameLimit);
        RuleFor(x => x.Category).MaximumLength(AppConstants.DataLimits.NameLimit);
    }
}