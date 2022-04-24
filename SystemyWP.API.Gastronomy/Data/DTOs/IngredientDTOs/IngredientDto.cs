namespace SystemyWP.API.Gastronomy.Data.DTOs.IngredientDTOs;

public class IngredientDto
{
    public string AccessKey { get; set; }
    
    public long Id { get; set; }
    
    public string Name { get; set; } 

    public string Description { get; set; }

    public MeasurementUnits MeasurementUnits { get; set; } = MeasurementUnits.None;

    public float PricePerStack { get; set; }
    public float StackSize { get; set; }
}