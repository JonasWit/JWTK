using Domain.MasterServiceShared.DTOs;
using FluentValidation;

namespace MasterService.FluentValidations.MasterService.UserForms;

public class UserChangePasswordFormValidation : AbstractValidator<UserChangePasswordForm>
{
    public UserChangePasswordFormValidation()
    {
        _ = RuleFor(x => x.OldPassword).NotEmpty().MinimumLength(12);
        _ = RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(12);
    }
}