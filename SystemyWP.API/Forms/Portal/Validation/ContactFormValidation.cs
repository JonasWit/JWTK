using FluentValidation;

namespace SystemyWP.API.Forms.Portal.Validation
{
    public class ContactFormValidation: AbstractValidator<ContactForm>
    {
        public ContactFormValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Phone).NotEmpty().MaximumLength(50);
        }
    }
}