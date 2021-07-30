using FluentValidation;

namespace SystemyWP.API.Forms.Portal.Validation
{
    public class PublicationFormValidation : AbstractValidator<PublicationForm>
    {
        public PublicationFormValidation()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.News).NotEmpty().MaximumLength(10000);
            RuleFor(x => x.PortalPublicationCategory).IsInEnum();
        }
    }
}