using FluentValidation;

namespace SystemyWP.API.Forms.Validation
{
    public class GrantDataAccessKeyValidation: AbstractValidator<GrantDataAccessKey>
    {
        public GrantDataAccessKeyValidation()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.DataAccessKey).NotEmpty();
        }
    }
}