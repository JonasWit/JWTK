using FluentValidation;

namespace SystemyWP.API.Forms.LegalApp.Reminders.Validation
{
    public class ReminderFormValidation : AbstractValidator<ReminderForm>
    {
        public ReminderFormValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Message).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Public).NotEmpty();
            RuleFor(x => x.Start).NotEmpty();
            RuleFor(x => x.End).NotEmpty();    
            RuleFor(x => x.ReminderCategory).NotNull();  
        }
    }
}