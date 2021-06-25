using FluentValidation;

namespace SystemyWP.API.Forms.GeneralApp.Contact.Validation
{
    public class ContactValidation : AbstractValidator<ContactForm>
    {
        public ContactValidation()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Name).MaximumLength(200);
            RuleFor(x => x.Surname).MaximumLength(200);
            RuleFor(x => x.Comment).MaximumLength(300);
        }
    }
}