using FluentValidation;

namespace SystemyWP.API.Forms.Admin.Validation
{
    public class UserIdFormValidation : AbstractValidator<UserPersonalDataForm>
    {
        public UserIdFormValidation()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}