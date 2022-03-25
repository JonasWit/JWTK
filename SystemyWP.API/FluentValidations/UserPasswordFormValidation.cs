using FluentValidation;
using SystemyWP.API.Forms;

namespace SystemyWP.API.FluentValidations;

public class UserPasswordFormValidation : AbstractValidator<UserPasswordForm>
{
    public UserPasswordFormValidation()
    {
        RuleFor(x => x.Password).NotEmpty().MinimumLength(12);
    }
}