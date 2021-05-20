using SystemyWP.API.Forms.GeneralApp.Contact;
using FluentValidation;

namespace SystemyWP.API.Forms.Validation.GeneralApp.Contact
{
    public class CreatePhysicalAddressValidation: AbstractValidator<CreatePhysicalAddressForm>
    {
        public CreatePhysicalAddressValidation()
        {
            RuleFor(x => x.Building).MaximumLength(50);
            RuleFor(x => x.Comment).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Country).MaximumLength(75);
            RuleFor(x => x.City).MaximumLength(75);
            RuleFor(x => x.Street).NotEmpty().MaximumLength(200);
            RuleFor(x => x.PostCode).MaximumLength(50);
        }
    }
}