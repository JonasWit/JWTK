using SystemyWP.API.Forms.GeneralApp.Contact;
using FluentValidation;

namespace SystemyWP.API.Forms.Validation.GeneralApp.Contact
{
    public class CreateContactPhoneNumberValidation : AbstractValidator<CreateContactPhoneNumberForm>
    {
        public CreateContactPhoneNumberValidation()
        {
            RuleFor(x => x.Number).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Comment).MaximumLength(100);
        }
    }
}