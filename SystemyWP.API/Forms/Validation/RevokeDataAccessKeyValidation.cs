using FluentValidation;

namespace SystemyWP.API.Forms.Validation
{
    public class RevokeDataAccessKeyValidation: AbstractValidator<RevokeDataAccessKeyForm>
    {
        public RevokeDataAccessKeyValidation()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}