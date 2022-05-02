using FluentValidation;
using SystemyWP.API.Gastronomy.Data.DTOs.DishDTOs;

namespace SystemyWP.API.Gastronomy.FluentValidations.Dish;

public class DishUpdateIngredientValidation: AbstractValidator<DishIngredientUpdateDto>
{
    public DishUpdateIngredientValidation()
    {
        RuleFor(x => x.AccessKey).NotEmpty();
        RuleFor(x => x.DishId).NotEmpty();
        RuleFor(x => x.IngredientId).NotEmpty();
    }
}