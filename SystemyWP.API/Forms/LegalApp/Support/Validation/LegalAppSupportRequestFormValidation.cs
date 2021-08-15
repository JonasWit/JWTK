using FluentValidation;

namespace SystemyWP.API.Forms.LegalApp.Support.Validation
{
    public class LegalAppSupportRequestFormValidation : AbstractValidator<LegalAppSupportRequestForm>
    {
        public LegalAppSupportRequestFormValidation()
        {
            RuleFor(x => x.Subject).NotEmpty().MaximumLength(500);
            RuleFor(x => x.Body).NotEmpty().MaximumLength(5000);
        }
    }
}