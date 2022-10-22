using FluentValidation;
using MasterService.API.Data.DTOs.General.UserForms;

namespace MasterService.API.FluentValidations.MasterService.UserForms;

public class UserPasswordFormValidation : AbstractValidator<UserPasswordForm>
{
    public UserPasswordFormValidation() => RuleFor(x => x.Password).NotEmpty().MinimumLength(12);
}