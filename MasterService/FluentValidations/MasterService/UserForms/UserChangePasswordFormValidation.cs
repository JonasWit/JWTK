using FluentValidation;
using MasterService.API.Data.DTOs.General.UserForms;

namespace MasterService.API.FluentValidations.MasterService.UserForms;

public class UserChangePasswordFormValidation : AbstractValidator<UserChangePasswordForm>
{
    public UserChangePasswordFormValidation()
    {
        RuleFor(x => x.OldPassword).NotEmpty().MinimumLength(12);
        RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(12);
    }
}