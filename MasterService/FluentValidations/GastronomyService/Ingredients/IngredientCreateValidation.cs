using FluentValidation;
using MasterService.API.Constants;
using MasterService.API.Data.DTOs.Gastronomy.Ingredients;

namespace MasterService.API.FluentValidations.GastronomyService.Ingredients;

public class IngredientCreateValidation : AbstractValidator<IngredientCreatePayload>
{
    public IngredientCreateValidation()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(AppConstants.DataLimits.NameLimit);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(AppConstants.DataLimits.DescriptionLimit);
        RuleFor(x => x.MeasurementUnits).IsInEnum();
        RuleFor(x => x.PricePerStack).NotNull().InclusiveBetween(0, 1000000);
        RuleFor(x => x.StackSize).NotNull().InclusiveBetween(0.001, 1000000);
        RuleFor(x => x.Category).MaximumLength(AppConstants.DataLimits.NameLimit);
    }
}