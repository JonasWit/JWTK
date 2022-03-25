using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SystemyWP.API.Gastronomy.Data.Models
{
    public class Menu : BaseModel
    {
        [Required] [MaxLength(SystemyWpConstants.DataLimits.NameLimit)] public string Name { get; set; } = "";

        [MaxLength(SystemyWpConstants.DataLimits.DescriptionLimit)] public string Description { get; set; } = "";

        public List<Dish> Dishes { get; set; } = new();
    }
}