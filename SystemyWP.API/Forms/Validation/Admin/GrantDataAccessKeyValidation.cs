using Systemywp.Api.Forms.Admin;
using FluentValidation;

namespace Systemywp.Api.Forms.Validation.Admin
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