using FluentValidation;
using SystemyWP.API.Gastronomy.DTOs;

namespace SystemyWP.API.Gastronomy.FluentValidations;

public class DishCreateDtoValidation: AbstractValidator<DishCreateDto>
{
    public DishCreateDtoValidation()
    {
        RuleFor(x => x.AccessKey).NotEmpty();
    }
}