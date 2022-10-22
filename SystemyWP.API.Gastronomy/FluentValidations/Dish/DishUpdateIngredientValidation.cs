using FluentValidation;
using MasterService.API.Gastronomy.Data.DTOs.DishDTOs;

namespace MasterService.API.Gastronomy.FluentValidations.Dish;

public class DishUpdateIngredientValidation: AbstractValidator<DishIngredientUpdateDto>
{
    public DishUpdateIngredientValidation()
    {
        RuleFor(x => x.AccessKey).NotEmpty();
        RuleFor(x => x.DishId).NotEmpty();
        RuleFor(x => x.IngredientId).NotEmpty();
    }
}