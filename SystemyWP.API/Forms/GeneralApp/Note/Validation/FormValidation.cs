using FluentValidation;

namespace SystemyWP.API.Forms.GeneralApp.Note.Validation
{
    public class FormValidation: AbstractValidator<NoteForm>
    {
        public FormValidation()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Message).MaximumLength(1000);
        }
    }
}