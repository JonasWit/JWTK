using FluentValidation;
using SystemyWP.API.Gastronomy.Data.DTOs.IngredientDTOs;

namespace SystemyWP.API.Gastronomy.FluentValidations.Ingredient;

public class IngredientDtoValidation : AbstractValidator<IngredientDto>
{
    public IngredientDtoValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.AccessKey).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(AppConstants.DataLimits.NameLimit);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(AppConstants.DataLimits.DescriptionLimit);
        RuleFor(x => x.MeasurementUnits).IsInEnum();
        RuleFor(x => x.PricePerStack).NotNull().InclusiveBetween(0, 1000000);
        RuleFor(x => x.StackSize).NotNull().InclusiveBetween(0.001, 1000000);
        RuleFor(x => x.Category).MaximumLength(AppConstants.DataLimits.NameLimit);
    }
}