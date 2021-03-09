using FluentValidation;

namespace SystemyWP.API.Forms.Validation
{
    public class EditAccessKeyValidation: AbstractValidator<EditAccessKeyForm>
    {
        public EditAccessKeyValidation()
        {
            RuleFor(x => x.NewKeyName).NotEmpty();
            RuleFor(x => x.OldKeyName).NotEmpty();
            RuleFor(x => x.ExpireDate).NotEmpty();
        }
    }
}