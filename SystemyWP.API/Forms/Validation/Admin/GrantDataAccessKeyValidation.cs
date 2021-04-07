using SystemyWP.API.Forms.Admin;
using FluentValidation;

namespace SystemyWP.API.Forms.Validation.Admin
{
    public class GrantDataAccessKeyValidation: AbstractValidator<GrantDataAccessKeyForm>
    {
        public GrantDataAccessKeyValidation()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.DataAccessKey).NotEmpty();
        }
    }
}