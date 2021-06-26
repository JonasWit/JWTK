using FluentValidation;

namespace SystemyWP.API.Forms.Admin.Validation
{
    public class UserPersonalDataFormValidation : AbstractValidator<UserPersonalDataForm>
    {
        public UserPersonalDataFormValidation()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}