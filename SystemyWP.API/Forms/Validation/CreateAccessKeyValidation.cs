using FluentValidation;

namespace SystemyWP.API.Forms.Validation
{
    public class CreateAccessKeyValidation: AbstractValidator<CreateAccessKeyForm>
    {
        public CreateAccessKeyValidation()
        {
            RuleFor(x => x.KeyName).NotEmpty();
            RuleFor(x => x.ExpireDate).NotEmpty();
        }
    }
}