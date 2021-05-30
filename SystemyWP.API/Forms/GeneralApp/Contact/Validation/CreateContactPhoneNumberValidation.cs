using FluentValidation;

namespace SystemyWP.API.Forms.GeneralApp.Contact.Validation
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