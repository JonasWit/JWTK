using SystemyWP.API.Forms.LegalApp;
using FluentValidation;

namespace SystemyWP.API.Forms.Validation.LegalApp
{
    public class CreateClientFormValidation: AbstractValidator<CreateClientForm>
    {
        public CreateClientFormValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}