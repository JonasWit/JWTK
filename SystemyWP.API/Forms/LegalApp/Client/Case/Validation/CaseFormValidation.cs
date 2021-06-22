using FluentValidation;

namespace SystemyWP.API.Forms.LegalApp.Client.Case.Validation
{
    public class CaseFormValidation : AbstractValidator<CaseForm>
    {
        public CaseFormValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}