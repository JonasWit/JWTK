using FluentValidation;
using SystemyWP.API.Forms;

namespace SystemyWP.API.FluentValidations;

public class UserPasswordResetFromValidation : AbstractValidator<UserPasswordResetForm>
{
    public UserPasswordResetFromValidation()
    {
        RuleFor(x => x.Token).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(12);
    }
}