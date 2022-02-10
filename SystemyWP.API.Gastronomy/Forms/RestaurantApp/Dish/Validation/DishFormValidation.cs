using FluentValidation;

namespace SystemyWP.API.Gastronomy.Forms.RestaurantApp.Dish.Validation
{
    public class DishFormValidation : AbstractValidator<DishForm>
    {
        public DishFormValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(500);
        }
    }
}