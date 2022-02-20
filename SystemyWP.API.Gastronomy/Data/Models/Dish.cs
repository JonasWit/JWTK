using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.Lib.Shared.DTOs.SharedConstants;

namespace SystemyWP.API.Gastronomy.Data.Models
{
    public class Dish : BaseModel
    {
        [Required] [MaxLength(SharedConstants.DataLimits.NameLimit)] public string Name { get; set; } = "";

        [MaxLength(SharedConstants.DataLimits.DescriptionLimit)] public string Description { get; set; } = "";

        public List<Ingredient> Ingredients { get; set; } = new();

        public List<Menu> Menus { get; set; } = new();
    }
}