using FluentValidation;
using SystemyWP.Lib.Shared.DTOs.Gastronomy;
using SystemyWP.Lib.Shared.DTOs.SharedConstants;

namespace SystemyWP.API.FluentValidations;

public class CreateIngredientDtoValidation: AbstractValidator<CreateIngredientDto>
{
    public CreateIngredientDtoValidation()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(SharedConstants.DataLimits.NameLimit);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(SharedConstants.DataLimits.DescriptionLimit);
    }
}