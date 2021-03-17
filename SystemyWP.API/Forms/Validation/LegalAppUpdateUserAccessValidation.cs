using FluentValidation;

namespace SystemyWP.API.Forms.Validation
{
    public class LegalAppUpdateUserAccessValidation: AbstractValidator<LegalAppUpdateUserAccess>
    {
        public LegalAppUpdateUserAccessValidation()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}