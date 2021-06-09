using FluentValidation;

namespace SystemyWP.API.Forms.LegalApp.Client.Validation
{
    public class ClientFormValidation : AbstractValidator<ClientForm>
    {
        public ClientFormValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}