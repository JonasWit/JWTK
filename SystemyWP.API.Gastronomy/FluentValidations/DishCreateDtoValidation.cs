using FluentValidation;
using SystemyWP.API.Gastronomy.DTOs.DishDTOs;

namespace SystemyWP.API.Gastronomy.FluentValidations;

public class DishCreateDtoValidation: AbstractValidator<DishCreateDto>
{
    public DishCreateDtoValidation()
    {
        RuleFor(x => x.AccessKey).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}