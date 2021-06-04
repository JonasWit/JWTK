using SystemyWP.API.Forms.LegalApp.Client;
using FluentValidation;

namespace SystemyWP.API.Forms.LegalApp.Case.Validation
{
    public class CaseFormValidation: AbstractValidator<CaseForm>
    {
        public CaseFormValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}