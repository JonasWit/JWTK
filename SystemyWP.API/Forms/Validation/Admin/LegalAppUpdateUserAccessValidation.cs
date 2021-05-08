using Systemywp.Api.Forms.Admin;
using FluentValidation;

namespace Systemywp.Api.Forms.Validation.Admin
{
    public class LegalAppUpdateUserAccessValidation: AbstractValidator<LegalAppUpdateUserAccessForm>
    {
        public LegalAppUpdateUserAccessValidation()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}