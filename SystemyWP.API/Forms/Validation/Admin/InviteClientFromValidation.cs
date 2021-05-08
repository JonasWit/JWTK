using Systemywp.Api.Forms.Admin;
using FluentValidation;

namespace Systemywp.Api.Forms.Validation.Admin
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