using FluentValidation;
using MasterService.API.Gastronomy.Data.DTOs.DishDTOs;

namespace MasterService.API.Gastronomy.FluentValidations.Dish;

public class DishBasicDtoValidation : AbstractValidator<DishBasicDto>
{
    public DishBasicDtoValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.AccessKey).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(AppConstants.DataLimits.NameLimit);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(AppConstants.DataLimits.DescriptionLimit);
        RuleFor(x => x.Category).MaximumLength(AppConstants.DataLimits.NameLimit);
    }
}