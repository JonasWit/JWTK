using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.API.Gastronomy.DTOs;

namespace SystemyWP.API.Gastronomy.Data.Models
{
    public class Ingredient : BaseModel
    {
        [Required] [MaxLength(SystemyWpConstants.DataLimits.NameLimit)] public string Name { get; set; } = "";

        [MaxLength(SystemyWpConstants.DataLimits.DescriptionLimit)] public string Description { get; set; } = "";

        public MeasurementUnits MeasurementUnits { get; set; } = MeasurementUnits.None;

        public float PricePerStack { get; set; }
        public float StackSize { get; set; }

        public List<Dish> Dishes { get; set; } = new();
    }
}