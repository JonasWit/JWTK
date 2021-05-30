using FluentValidation;

namespace SystemyWP.API.Forms.GeneralApp.Access.Validation
{
    public class EditAccessKeyValidation: AbstractValidator<EditAccessKeyForm>
    {
        public EditAccessKeyValidation()
        {
            RuleFor(x => x.NewKeyName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.ExpireDate).NotEmpty();
        }
    }
}