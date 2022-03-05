using FluentValidation;
using SystemyWP.API.Forms;

namespace SystemyWP.API.FluentValidations;

public class UserEmailFormValidation : AbstractValidator<UserEmailForm>
{
    public UserEmailFormValidation()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}