using FluentValidation;
using MasterService.API.Data.DTOs.General.UserForms;

namespace MasterService.API.FluentValidations.MasterService.UserForms;

public class UserPasswordResetFromValidation : AbstractValidator<UserPasswordResetForm>
{
    public UserPasswordResetFromValidation()
    {
        RuleFor(x => x.Token).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(12);
    }
}