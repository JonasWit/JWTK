using Systemywp.Api.Forms.Admin;
using FluentValidation;

namespace Systemywp.Api.Forms.Validation.Admin
{
    public class UserPersonalDataFormValidation: AbstractValidator<UserPersonalDataForm>
    {
        public UserPersonalDataFormValidation()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}