using SystemyWP.API.Forms.Admin;
using FluentValidation;

namespace SystemyWP.API.Forms.Validation.Admin
{
    public class RevokeDataAccessKeyValidation: AbstractValidator<RevokeDataAccessKeyForm>
    {
        public RevokeDataAccessKeyValidation()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}