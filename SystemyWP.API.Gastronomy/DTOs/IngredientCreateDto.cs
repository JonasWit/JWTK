namespace SystemyWP.API.Gastronomy.DTOs;

public class CreateIngredientDto : BasicDto
{
    public string Name { get; set; } 

    public string Description { get; set; } 

    public MeasurementUnits MeasurementUnits { get; set; } = MeasurementUnits.None;

    public float PricePerStack { get; set; }
    public float StackSize { get; set; }
}