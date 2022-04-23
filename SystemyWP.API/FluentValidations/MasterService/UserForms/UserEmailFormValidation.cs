using FluentValidation;
using SystemyWP.API.DTOs.General.UserForms;

namespace SystemyWP.API.FluentValidations.MasterService.UserForms;

public class UserEmailFormValidation : AbstractValidator<UserEmailForm>
{
    public UserEmailFormValidation()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}