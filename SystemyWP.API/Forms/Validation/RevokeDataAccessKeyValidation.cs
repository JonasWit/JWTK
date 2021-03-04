using FluentValidation;

namespace SystemyWP.API.Forms.Validation
{
    public class RevokeDataAccessKeyValidation: AbstractValidator<RevokeDataAccessKey>
    {
        public RevokeDataAccessKeyValidation()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}