using SystemyWP.API.Forms.LegalApp.Client;
using FluentValidation;

namespace SystemyWP.API.Forms.Validation.LegalApp.Client
{
    public class UpdateClientFormValidation: AbstractValidator<UpdateClientForm>
    {
        public UpdateClientFormValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}