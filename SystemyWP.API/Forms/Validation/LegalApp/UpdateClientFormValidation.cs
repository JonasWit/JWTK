using Systemywp.Api.Forms.LegalApp;
using FluentValidation;

namespace Systemywp.Api.Forms.Validation.LegalApp
{
    public class UpdateClientFormValidation: AbstractValidator<UpdateClientForm>
    {
        public UpdateClientFormValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}