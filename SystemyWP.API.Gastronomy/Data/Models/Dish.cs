using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SystemyWP.API.Gastronomy.Data.Models
{
    public class Dish : BaseModel
    {
        [Required] [MaxLength(AppConstants.DataLimits.NameLimit)] public string Name { get; set; } = "";

        [MaxLength(AppConstants.DataLimits.DescriptionLimit)] public string Description { get; set; } = "";

        public List<Ingredient> Ingredients { get; set; } = new();

        public List<Menu> Menus { get; set; } = new();
    }
}