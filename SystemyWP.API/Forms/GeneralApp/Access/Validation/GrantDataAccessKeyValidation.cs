using FluentValidation;

namespace SystemyWP.API.Forms.GeneralApp.Access.Validation
{
    public class GrantDataAccessKeyValidation: AbstractValidator<GrantDataAccessKeyForm>
    {
        public GrantDataAccessKeyValidation()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.DataAccessKey).NotEmpty().MaximumLength(50);
        }
    }
}