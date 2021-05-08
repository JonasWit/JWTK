using SystemyWP.API.Forms.Admin;
using FluentValidation;

namespace SystemyWP.API.Forms.Validation.Admin
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