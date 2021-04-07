using SystemyWP.API.Forms.Admin;
using FluentValidation;

namespace SystemyWP.API.Forms.Validation.Admin
{
    public class CreateAccessKeyValidation: AbstractValidator<AccessKeyForm>
    {
        public CreateAccessKeyValidation()
        {
            RuleFor(x => x.KeyName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.ExpireDate).NotEmpty();
        }
    }
}