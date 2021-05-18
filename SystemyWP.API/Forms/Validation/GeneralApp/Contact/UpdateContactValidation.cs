using SystemyWP.API.Forms.LegalApp.Contact;
using FluentValidation;

namespace SystemyWP.API.Forms.Validation.LegalApp.Contact
{
    public class UpdateContactValidation: AbstractValidator<UpdateContactForm>
    {
        public UpdateContactValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Comment).MaximumLength(300);
        }
    }
}