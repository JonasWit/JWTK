using Systemywp.Api.Forms.Portal;
using FluentValidation;

namespace Systemywp.Api.Forms.Validation.Portal
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