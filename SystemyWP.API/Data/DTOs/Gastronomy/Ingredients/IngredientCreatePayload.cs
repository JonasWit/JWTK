using SystemyWP.API.Constants;

namespace SystemyWP.API.Data.DTOs.Gastronomy.Ingredients;

public class IngredientCreatePayload
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public MeasurementUnits MeasurementUnits { get; set; } = MeasurementUnits.None;
    public int PricePerStack { get; set; }
    public double StackSize { get; set; }
}