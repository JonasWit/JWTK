using SystemyWP.API.Forms.LegalApp.Client;
using FluentValidation;

namespace SystemyWP.API.Forms.Validation.LegalApp.Client
{
    public class CreateClientFormValidation: AbstractValidator<CreateClientForm>
    {
        public CreateClientFormValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}