using FluentValidation;
using MasterService.API.Data.DTOs.General.UserForms;

namespace MasterService.API.FluentValidations.MasterService.UserForms;

public class UserEmailFormValidation : AbstractValidator<UserEmailForm>
{
    public UserEmailFormValidation()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}