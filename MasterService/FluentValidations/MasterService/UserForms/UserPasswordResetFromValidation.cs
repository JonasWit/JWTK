using Domain.Shared.MasterServiceShared.DTOs;
using FluentValidation;

namespace MasterService.API.FluentValidations.MasterService.UserForms;

public class UserPasswordResetFromValidation : AbstractValidator<UserPasswordResetForm>
{
    public UserPasswordResetFromValidation()
    {
        RuleFor(x => x.Token).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(12);
    }
}