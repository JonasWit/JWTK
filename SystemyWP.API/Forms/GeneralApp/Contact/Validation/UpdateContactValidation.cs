using FluentValidation;

namespace SystemyWP.API.Forms.GeneralApp.Contact.Validation
{
    public class UpdateContactValidation: AbstractValidator<UpdateContactForm>
    {
        public UpdateContactValidation()
        {
            RuleFor(x => x.Title).MaximumLength(200);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Surname).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Comment).MaximumLength(300);
        }
    }
}