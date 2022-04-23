using FluentValidation;
using SystemyWP.API.DTOs.General.UserForms;

namespace SystemyWP.API.FluentValidations.MasterService.UserForms;

public class UserPasswordResetFromValidation : AbstractValidator<UserPasswordResetForm>
{
    public UserPasswordResetFromValidation()
    {
        RuleFor(x => x.Token).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(12);
    }
}