using Domain.MasterServiceShared.DTOs;
using FluentValidation;

namespace MasterService.FluentValidations.MasterService.UserForms;

public class UserPasswordFormValidation : AbstractValidator<UserPasswordForm>
{
    public UserPasswordFormValidation() => RuleFor(x => x.Password).NotEmpty().MinimumLength(12);
}