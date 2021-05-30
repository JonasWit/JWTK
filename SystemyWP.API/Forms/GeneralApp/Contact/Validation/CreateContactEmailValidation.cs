using FluentValidation;

namespace SystemyWP.API.Forms.GeneralApp.Contact.Validation
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