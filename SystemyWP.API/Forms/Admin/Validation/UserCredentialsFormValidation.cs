using FluentValidation;

namespace SystemyWP.API.Forms.Admin.Validation
{
    public class UserCredentialsFormValidation : AbstractValidator<UserCredentialsForm>
    {
        public UserCredentialsFormValidation()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}