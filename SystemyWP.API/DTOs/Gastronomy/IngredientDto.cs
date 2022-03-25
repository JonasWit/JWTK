using SystemyWP.API.Constants;

namespace SystemyWP.API.DTOs.Gastronomy;

public class IngredientDto : BaseGastronomyDto
{
    public string Name { get; set; } 

    public string Description { get; set; } 

    public MeasurementUnits MeasurementUnits { get; set; } 

    public float PricePerStack { get; set; }
    public float StackSize { get; set; }
}