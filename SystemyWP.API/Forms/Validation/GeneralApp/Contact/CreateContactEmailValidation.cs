using SystemyWP.API.Forms.GeneralApp.Contact;
using FluentValidation;

namespace SystemyWP.API.Forms.Validation.GeneralApp.Contact
{
    public class CreateContactEmailValidation : AbstractValidator<CreateContactEmailFrom>
    {
        public CreateContactEmailValidation()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(256);
            RuleFor(x => x.Comment).MaximumLength(100);
        }
    }
}