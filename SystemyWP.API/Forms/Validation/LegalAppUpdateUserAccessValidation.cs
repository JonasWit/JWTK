using FluentValidation;

namespace SystemyWP.API.Forms.Validation
{
    public class LegalAppUpdateUserAccessValidation: AbstractValidator<LegalAppUpdateUserAccessForm>
    {
        public LegalAppUpdateUserAccessValidation()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}