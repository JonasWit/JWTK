using FluentValidation;

namespace SystemyWP.API.Forms.LegalApp.Access.Validation
{
    public class LegalAppCreateAccessKeyValidation: AbstractValidator<LegalAppAccessKeyForm>
    {
        public LegalAppCreateAccessKeyValidation()
        {
            RuleFor(x => x.KeyName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.ExpireDate).NotEmpty();
        }
    }
}