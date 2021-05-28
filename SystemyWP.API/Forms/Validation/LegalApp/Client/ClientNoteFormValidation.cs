using SystemyWP.API.Forms.LegalApp.Client;
using FluentValidation;

namespace SystemyWP.API.Forms.Validation.LegalApp.Client
{
    public class ClientNoteFormValidation: AbstractValidator<ClientNoteForm>
    {
        public ClientNoteFormValidation()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Message).MaximumLength(1000);
        }
    }
}