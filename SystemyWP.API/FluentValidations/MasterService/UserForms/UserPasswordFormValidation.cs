using FluentValidation;
using SystemyWP.API.Data.DTOs.General.UserForms;

namespace SystemyWP.API.FluentValidations.MasterService.UserForms;

public class UserPasswordFormValidation : AbstractValidator<UserPasswordForm>
{
    public UserPasswordFormValidation() => RuleFor(x => x.Password).NotEmpty().MinimumLength(12);
}