using FluentValidation;

namespace SystemyWP.API.Forms.User.Access.Validation
{
    public class CreateAccessKeyValidation: AbstractValidator<CreateAccessKeyForm>
    {
        public CreateAccessKeyValidation()
        {
            RuleFor(x => x.KeyName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.ExpireDate).NotEmpty();
        }
    }
}