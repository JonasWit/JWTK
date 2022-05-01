using FluentValidation;
using SystemyWP.API.Constants;
using SystemyWP.API.Data.DTOs.Gastronomy.Ingredients;

namespace SystemyWP.API.FluentValidations.GastronomyService.Ingredients;

public class IngredientValidation: AbstractValidator<IngredientDto>
{
    public IngredientValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(AppConstants.DataLimits.NameLimit);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(AppConstants.DataLimits.DescriptionLimit);
        RuleFor(x => x.MeasurementUnits).IsInEnum();
        RuleFor(x => x.PricePerStack).NotNull().InclusiveBetween(0, 1000000);     
        RuleFor(x => x.StackSize).NotNull().InclusiveBetween(0.001, 1000000);   
    }
}