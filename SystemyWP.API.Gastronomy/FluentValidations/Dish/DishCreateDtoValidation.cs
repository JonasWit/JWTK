using FluentValidation;
using SystemyWP.API.Gastronomy.Data.DTOs.DishDTOs;

namespace SystemyWP.API.Gastronomy.FluentValidations.Dish;

public class DishCreateDtoValidation : AbstractValidator<DishCreateDto>
{
    public DishCreateDtoValidation()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(AppConstants.DataLimits.NameLimit);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(AppConstants.DataLimits.DescriptionLimit);
        RuleFor(x => x.Category).MaximumLength(AppConstants.DataLimits.NameLimit);
    }
}