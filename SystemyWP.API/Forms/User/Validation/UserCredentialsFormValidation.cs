using FluentValidation;

namespace SystemyWP.API.Forms.User.Validation
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