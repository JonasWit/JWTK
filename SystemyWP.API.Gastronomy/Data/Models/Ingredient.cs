using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.API.Gastronomy.Data.DTOs;

namespace SystemyWP.API.Gastronomy.Data.Models
{
    public record Ingredient : BaseModel
    {
        [Required]
        [MaxLength(AppConstants.DataLimits.NameLimit)]
        public string Name { get; set; } = "";

        [MaxLength(AppConstants.DataLimits.DescriptionLimit)]
        public string Description { get; set; } = "";

        public MeasurementUnits MeasurementUnits { get; set; } = MeasurementUnits.None;

        public string Image { get; set; }

        public int PricePerStack { get; set; }
        public double StackSize { get; set; }

        public List<Dish> Dishes { get; set; } = new();
    }
}