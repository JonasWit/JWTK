using SystemyWP.API.Forms.LegalApp.Client;
using SystemyWP.API.Forms.LegalApp.Contact;
using FluentValidation;

namespace SystemyWP.API.Forms.Validation.LegalApp.Contact
{
    public class CreateContactValidation: AbstractValidator<CreateContactForm>
    {
        public CreateContactValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Comment).MaximumLength(300);
        }
    }
}