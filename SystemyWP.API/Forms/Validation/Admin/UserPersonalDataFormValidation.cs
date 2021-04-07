using SystemyWP.API.Forms.Admin;
using FluentValidation;

namespace SystemyWP.API.Forms.Validation.Admin
{
    public class UserPersonalDataFormValidation: AbstractValidator<UserPersonalDataForm>
    {
        public UserPersonalDataFormValidation()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}