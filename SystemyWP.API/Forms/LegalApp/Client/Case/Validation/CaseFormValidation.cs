using FluentValidation;

namespace SystemyWP.API.Forms.LegalApp.Client.Case.Validation
{
    public class CaseFormValidation : AbstractValidator<CaseForm>
    {
        public CaseFormValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Group).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Signature).MaximumLength(200);
            RuleFor(x => x.Description).MaximumLength(1000);
        }
    }
}