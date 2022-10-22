using FluentValidation;
using MasterService.API.Data.DTOs.General.UserForms;

namespace MasterService.API.FluentValidations.MasterService.UserForms;

public class UserCredentialsFormValidation : AbstractValidator<UserCredentialsForm>
{
    public UserCredentialsFormValidation()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(12);
    }
}