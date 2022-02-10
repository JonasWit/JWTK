using FluentValidation;

namespace SystemyWP.API.Gastronomy.Forms.RestaurantApp.Menu.Validation
{
    public class MenuFormValidation : AbstractValidator<MenuForm>
    {
        public MenuFormValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(500);
        }
    }
}