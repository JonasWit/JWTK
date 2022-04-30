using FluentValidation;
using SystemyWP.API.Constants;
using SystemyWP.API.Data.DTOs.Gastronomy;
using SystemyWP.API.Data.DTOs.Gastronomy.Dishes;

namespace SystemyWP.API.FluentValidations.GastronomyService;

public class DishCreateValidation : AbstractValidator<DishCreateDto>
{
    public DishCreateValidation()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(AppConstants.DataLimits.NameLimit);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(AppConstants.DataLimits.DescriptionLimit);
    }
}