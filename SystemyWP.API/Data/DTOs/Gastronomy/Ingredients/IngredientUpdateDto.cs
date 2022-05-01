using SystemyWP.API.Constants;

namespace SystemyWP.API.Data.DTOs.Gastronomy.Ingredients;

public class IngredientUpdateDto
{
    public long Id { get; set; }
    public string AccessKey { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public MeasurementUnits MeasurementUnits { get; set; }
    public int PricePerStack { get; set; }
    public double StackSize { get; set; }
}