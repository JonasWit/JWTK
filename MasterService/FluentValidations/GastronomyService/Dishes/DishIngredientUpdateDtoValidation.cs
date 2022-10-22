using FluentValidation;
using MasterService.API.Data.DTOs.Gastronomy.Dishes;

namespace MasterService.API.FluentValidations.GastronomyService.Dishes;

public class DishIngredientUpdateDtoValidation : AbstractValidator<DishIngredientUpdatePayload>
{
    public DishIngredientUpdateDtoValidation()
    {
        RuleFor(x => x.DishId).NotEmpty();
        RuleFor(x => x.IngredientId).NotEmpty();
    }
}