using SystemyWP.API.Forms.LegalApp.Contact;
using FluentValidation;

namespace SystemyWP.API.Forms.Validation.LegalApp.Contact
{
    public class CreateContactEmailValidation : AbstractValidator<CreateContactEmailFrom>
    {
        public CreateContactEmailValidation()
        {
            RuleFor(x => x.Email).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Comment).MaximumLength(300);
        }
    }
}