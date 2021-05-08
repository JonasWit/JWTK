using Systemywp.Api.Forms.Admin;
using FluentValidation;

namespace Systemywp.Api.Forms.Validation.Admin
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