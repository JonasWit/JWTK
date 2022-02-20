using FluentValidation;
using SystemyWP.API.Forms;

namespace SystemyWP.API.FluentValidations;

public class UserChangePasswordFormValidation : AbstractValidator<UserChangePasswordForm>
{
    public UserChangePasswordFormValidation()
    {
        RuleFor(x => x.OldPassword).NotEmpty().MinimumLength(12);
        RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(12);
    }
}