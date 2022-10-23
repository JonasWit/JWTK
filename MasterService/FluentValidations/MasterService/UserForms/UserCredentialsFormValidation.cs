using Domain.MasterServiceShared.DTOs;
using FluentValidation;

namespace MasterService.FluentValidations.MasterService.UserForms;

public class UserCredentialsFormValidation : AbstractValidator<UserCredentialsForm>
{
    public UserCredentialsFormValidation()
    {
        _ = RuleFor(x => x.Email).NotEmpty().EmailAddress();
        _ = RuleFor(x => x.Password).NotEmpty().MinimumLength(12);
    }
}