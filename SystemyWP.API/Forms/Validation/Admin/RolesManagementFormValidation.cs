using Systemywp.Api.Forms.Admin;
using FluentValidation;

namespace Systemywp.Api.Forms.Validation.Admin
{
    public class RolesManagementFormValidation: AbstractValidator<RolesManagementForm>
    {
        public RolesManagementFormValidation()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Role).NotEmpty();
        }
    }
}