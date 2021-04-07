using SystemyWP.API.Forms.Admin;
using FluentValidation;

namespace SystemyWP.API.Forms.Validation.Admin
{
    public class LegalAppUpdateUserAccessValidation: AbstractValidator<LegalAppUpdateUserAccessForm>
    {
        public LegalAppUpdateUserAccessValidation()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}