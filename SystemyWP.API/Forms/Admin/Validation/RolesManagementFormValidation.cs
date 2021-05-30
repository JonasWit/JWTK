using FluentValidation;

namespace SystemyWP.API.Forms.Admin.Validation
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