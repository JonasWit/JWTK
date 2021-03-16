using FluentValidation;

namespace SystemyWP.API.Forms.Validation
{
    public class LightModeSwitchValidation: AbstractValidator<LightModeSwitchForm>
    {
        public LightModeSwitchValidation()
        {
            RuleFor(x => x.LightMode).NotNull();
        }
    }
}