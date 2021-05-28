using SystemyWP.API.Forms.LegalApp.Client;
using FluentValidation;

namespace SystemyWP.API.Forms.Validation.LegalApp.Client
{
    public class ClientWorkFormValidation : AbstractValidator<ClientWorkForm>
    {
        public ClientWorkFormValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).MaximumLength(300);
            RuleFor(x => x.Hours);
            RuleFor(x => x.Hours);
            RuleFor(x => x.Minutes);
            RuleFor(x => x.Amount);
            RuleFor(x => x.Rate);
            RuleFor(x => x.EventDate).NotEmpty();
        }
    }
}