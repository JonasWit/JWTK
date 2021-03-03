using FluentValidation;

namespace SystemyWP.API.Forms.Validation
{
    public class InviteClientFromValidation: AbstractValidator<InviteClientForm>
    {
        public InviteClientFromValidation()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.ReturnUrl).NotEmpty();
        }
    }
}