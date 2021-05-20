using SystemyWP.API.Forms.LegalApp.Client;
using FluentValidation;

namespace SystemyWP.API.Forms.Validation.LegalApp.Client
{
    public class CreateClientFinanceFormValidation: AbstractValidator<CreateClientFinanceForm>
    {
        public CreateClientFinanceFormValidation()
        {
            RuleFor(x => x.Amount);
            RuleFor(x => x.Hours);
            RuleFor(x => x.Amount);
        }
    }
}