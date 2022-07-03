namespace SystemyWP.API.Gastronomy.Data.DTOs.IngredientDTOs;

public class IngredientCreateDto
{
    public string AccessKey { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public MeasurementUnits MeasurementUnits { get; set; } = MeasurementUnits.None;
    public int PricePerStack { get; set; }
    public double StackSize { get; set; }
    public string Category { get; set; }
}