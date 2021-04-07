using SystemyWP.API.Forms.Admin;
using SystemyWP.API.Forms.LegalApp;
using FluentValidation;

namespace SystemyWP.API.Forms.Validation.Admin
{
    public class CreateClientFormValidation: AbstractValidator<CreateClientForm>
    {
        public CreateClientFormValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.AccessKeyId).NotEqual(0);
        }
    }
}