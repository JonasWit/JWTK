using FluentValidation;

namespace SystemyWP.API.Forms.LegalApp.Access.Validation
{
    public class LegalAppRevokeDataAccessKeyValidation: AbstractValidator<LegalAppRevokeDataAccessKeyForm>
    {
        public LegalAppRevokeDataAccessKeyValidation()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}