using FluentValidation;
using SystemyWP.API.Constants;
using SystemyWP.API.Data.DTOs.Gastronomy.Dishes;

namespace SystemyWP.API.FluentValidations.GastronomyService.Dishes;

public class DishIngredientUpdateDtoValidation : AbstractValidator<DishIngredientUpdateDto>
{
    public DishIngredientUpdateDtoValidation()
    {
        RuleFor(x => x.DishId).NotEmpty();
        RuleFor(x => x.IngredientId).NotEmpty();
    }
}