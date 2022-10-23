using Domain.MasterServiceShared.DTOs;
using FluentValidation;

namespace MasterService.FluentValidations.MasterService.UserForms;

public class UserEmailFormValidation : AbstractValidator<UserEmailForm>
{
    public UserEmailFormValidation() => RuleFor(x => x.Email).NotEmpty().EmailAddress();
}