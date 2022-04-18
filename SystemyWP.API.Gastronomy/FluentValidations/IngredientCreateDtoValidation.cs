using FluentValidation;
using SystemyWP.API.Gastronomy.DTOs.IngredientDTOs;

namespace SystemyWP.API.Gastronomy.FluentValidations;

public class IngredientCreateDtoValidation : AbstractValidator<IngredientCreateDto>
{
    public IngredientCreateDtoValidation()
    {
        RuleFor(x => x.AccessKey).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}