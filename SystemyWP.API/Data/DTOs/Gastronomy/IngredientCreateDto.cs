using SystemyWP.API.Constants;

namespace SystemyWP.API.Data.DTOs.Gastronomy;

public class IngredientCreateDto
{
    public string AccessKey { get; set; }
    
    public string Name { get; set; } 

    public string Description { get; set; } 

    public MeasurementUnits MeasurementUnits { get; set; } = MeasurementUnits.None;

    public int PricePerStack { get; set; }
    
    public double StackSize { get; set; }
}