using FluentValidation;
using SystemyWP.API.Forms;

namespace SystemyWP.API.FluentValidations;

public class UserCredentialsFormValidation : AbstractValidator<UserCredentialsForm>
{
    public UserCredentialsFormValidation()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(12);
    }
}