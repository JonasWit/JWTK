using FluentValidation;

namespace SystemyWP.API.Forms.LegalApp.Client.Case.Validation
{
    public class DeadlineFromValidation: AbstractValidator<DeadlineFrom>
    {
        public DeadlineFromValidation()
        {
            RuleFor(x => x.Message).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Deadline).NotEmpty();
        }
    }
}