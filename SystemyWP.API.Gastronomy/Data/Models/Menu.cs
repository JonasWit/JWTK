using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SystemyWP.API.Gastronomy.Data.Models
{
    public record Menu : BaseModel
    {
        [Required]
        [MaxLength(AppConstants.DataLimits.NameLimit)]
        public string Name { get; set; } = "";

        [MaxLength(AppConstants.DataLimits.DescriptionLimit)]
        public string Description { get; set; } = "";

        public string Image { get; set; }

        public List<Dish> Dishes { get; set; } = new();
    }
}