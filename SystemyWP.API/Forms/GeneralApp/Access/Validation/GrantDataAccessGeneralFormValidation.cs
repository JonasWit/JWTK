using FluentValidation;

namespace SystemyWP.API.Forms.GeneralApp.Access.Validation
{
    public class GrantDataAccessGeneralFormValidation : AbstractValidator<GrantDataAccessGeneralForm>
    {
        public GrantDataAccessGeneralFormValidation()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.ItemId).NotEmpty();
        }
    }
}