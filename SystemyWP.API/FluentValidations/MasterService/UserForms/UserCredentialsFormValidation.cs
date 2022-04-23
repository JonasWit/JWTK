using FluentValidation;
using SystemyWP.API.DTOs.General.UserForms;

namespace SystemyWP.API.FluentValidations.MasterService.UserForms;

public class UserCredentialsFormValidation : AbstractValidator<UserCredentialsForm>
{
    public UserCredentialsFormValidation()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(12);
    }
}