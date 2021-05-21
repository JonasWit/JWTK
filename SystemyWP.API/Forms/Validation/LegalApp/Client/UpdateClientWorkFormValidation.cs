using SystemyWP.API.Forms.LegalApp.Client;
using FluentValidation;

namespace SystemyWP.API.Forms.Validation.LegalApp.Client
{
    public class UpdateClientWorkFormValidation : AbstractValidator<UpdateClientWorkForm>
    {
        public UpdateClientWorkFormValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).MaximumLength(300);
            RuleFor(x => x.Hours);
            RuleFor(x => x.Minutes);
            RuleFor(x => x.Amount);
            RuleFor(x => x.Rate);
            RuleFor(x => x.EventDate).NotEmpty();
        }
    }
}