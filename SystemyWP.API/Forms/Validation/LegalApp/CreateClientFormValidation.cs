using Systemywp.Api.Forms.LegalApp;
using FluentValidation;

namespace Systemywp.Api.Forms.Validation.LegalApp
{
    public class CreateClientFormValidation: AbstractValidator<CreateClientForm>
    {
        public CreateClientFormValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}