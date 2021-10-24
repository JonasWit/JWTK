using FluentValidation;

namespace SystemyWP.API.Forms.LegalApp.Client.Validation
{
    public class ClientWorkFormValidation : AbstractValidator<ClientWorkForm>
    {
        public ClientWorkFormValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.LawyerName).MaximumLength(300);
            RuleFor(x => x.Description).MaximumLength(500);
            RuleFor(x => x.Hours).GreaterThan(-1).LessThan(int.MaxValue);
            RuleFor(x => x.Minutes).GreaterThan(-1).LessThan(60);
            RuleFor(x => x.Amount).GreaterThan(-1).LessThan(int.MaxValue);
            RuleFor(x => x.Rate).GreaterThan(-1).LessThan(5000);
            RuleFor(x => x.EventDate).NotEmpty();
        }
    }
}