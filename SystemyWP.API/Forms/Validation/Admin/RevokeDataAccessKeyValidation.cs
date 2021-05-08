using Systemywp.Api.Forms.Admin;
using FluentValidation;

namespace Systemywp.Api.Forms.Validation.Admin
{
    public class RevokeDataAccessKeyValidation: AbstractValidator<RevokeDataAccessKeyForm>
    {
        public RevokeDataAccessKeyValidation()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}