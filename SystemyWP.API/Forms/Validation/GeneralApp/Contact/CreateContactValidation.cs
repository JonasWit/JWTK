using SystemyWP.API.Forms.GeneralApp.Contact;
using FluentValidation;

namespace SystemyWP.API.Forms.Validation.GeneralApp.Contact
{
    public class CreateContactValidation : AbstractValidator<CreateContactForm>
    {
        public CreateContactValidation()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Name).MaximumLength(200);
            RuleFor(x => x.Surname).MaximumLength(200);
            RuleFor(x => x.Comment).MaximumLength(300);
        }
    }
}