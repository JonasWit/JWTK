using FluentValidation;

namespace SystemyWP.API.Forms.User.Validation;

public class UserChangePasswordFormValidation : AbstractValidator<UserChangePasswordForm>
{
    public UserChangePasswordFormValidation()
    {
        RuleFor(x => x.OldPassword).NotEmpty().MinimumLength(12);
        RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(12);
    }
}