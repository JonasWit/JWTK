using FluentValidation;

namespace SystemyWP.API.Forms.GeneralApp.Access.Validation
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