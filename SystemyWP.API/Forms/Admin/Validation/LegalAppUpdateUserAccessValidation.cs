using FluentValidation;

namespace SystemyWP.API.Forms.Admin.Validation
{
    public class LegalAppUpdateUserAccessValidation: AbstractValidator<LegalAppUpdateUserAccessForm>
    {
        public LegalAppUpdateUserAccessValidation()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}