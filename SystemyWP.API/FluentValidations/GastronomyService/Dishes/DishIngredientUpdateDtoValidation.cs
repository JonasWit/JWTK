using FluentValidation;
using SystemyWP.API.Data.DTOs.Gastronomy.Dishes;

namespace SystemyWP.API.FluentValidations.GastronomyService.Dishes;

public class DishIngredientUpdateDtoValidation : AbstractValidator<DishIngredientUpdatePayload>
{
    public DishIngredientUpdateDtoValidation()
    {
        RuleFor(x => x.DishId).NotEmpty();
        RuleFor(x => x.IngredientId).NotEmpty();
    }
}