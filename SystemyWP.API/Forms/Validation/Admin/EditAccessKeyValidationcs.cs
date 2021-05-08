using SystemyWP.API.Forms.Admin;
using FluentValidation;

namespace SystemyWP.API.Forms.Validation.Admin
{
    public class EditAccessKeyValidation: AbstractValidator<EditAccessKeyForm>
    {
        public EditAccessKeyValidation()
        {
            RuleFor(x => x.NewKeyName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.OldKeyName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.ExpireDate).NotEmpty();
        }
    }
}