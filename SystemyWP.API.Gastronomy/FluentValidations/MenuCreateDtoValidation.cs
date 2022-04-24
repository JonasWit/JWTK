using FluentValidation;
using SystemyWP.API.Gastronomy.Data.DTOs.MenuDTOs;

namespace SystemyWP.API.Gastronomy.FluentValidations;

public class MenuCreateDtoValidation : AbstractValidator<MenuCreateDto>
{
    public MenuCreateDtoValidation()
    {
        RuleFor(x => x.AccessKey).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}