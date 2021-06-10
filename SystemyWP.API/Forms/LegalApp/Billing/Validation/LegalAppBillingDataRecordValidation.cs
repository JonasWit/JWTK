using FluentValidation;

namespace SystemyWP.API.Forms.LegalApp.Billing.Validation
{
    public class LegalAppBillingDataRecordValidation : AbstractValidator<LegalAppBillingDataRecordForm>
    {
        public LegalAppBillingDataRecordValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Street).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Street).NotEmpty().MaximumLength(200);
            RuleFor(x => x.City).MaximumLength(100);
            RuleFor(x => x.Nip).MaximumLength(50);     
            RuleFor(x => x.Regon).MaximumLength(50);   
            RuleFor(x => x.PhoneNumber).MaximumLength(50);   
            RuleFor(x => x.PostalCode).MaximumLength(50);  
            RuleFor(x => x.FaxNumber).MaximumLength(50);  
        }
    }
}