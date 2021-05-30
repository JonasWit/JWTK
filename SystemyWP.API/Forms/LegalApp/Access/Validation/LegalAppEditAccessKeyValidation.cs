using FluentValidation;

namespace SystemyWP.API.Forms.LegalApp.Access.Validation
{
    public class LegalAppEditAccessKeyValidation: AbstractValidator<LegalAppEditAccessKeyForm>
    {
        public LegalAppEditAccessKeyValidation()
        {
            RuleFor(x => x.NewKeyName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.OldKeyName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.ExpireDate).NotEmpty();
        }
    }
}