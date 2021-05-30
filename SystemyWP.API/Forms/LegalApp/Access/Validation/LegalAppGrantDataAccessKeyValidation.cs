using FluentValidation;

namespace SystemyWP.API.Forms.LegalApp.Access.Validation
{
    public class LegalAppGrantDataAccessKeyValidation: AbstractValidator<LegalAppGrantDataAccessKeyForm>
    {
        public LegalAppGrantDataAccessKeyValidation()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.DataAccessKey).NotEmpty().MaximumLength(50);
        }
    }
}