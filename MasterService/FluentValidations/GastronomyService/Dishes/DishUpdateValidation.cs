using FluentValidation;
using MasterService.API.Constants;
using MasterService.API.Data.DTOs.Gastronomy.Dishes;

namespace MasterService.API.FluentValidations.GastronomyService.Dishes;

public class DishUpdateValidation : AbstractValidator<DishUpdatePayload>
{
    public DishUpdateValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(AppConstants.DataLimits.NameLimit);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(AppConstants.DataLimits.DescriptionLimit);
        RuleFor(x => x.Category).MaximumLength(AppConstants.DataLimits.NameLimit);
    }
}