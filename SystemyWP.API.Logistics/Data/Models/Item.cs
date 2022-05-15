namespace SystemyWP.API.Logistics.Data.Models;

public record Item : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; }   = string.Empty;
    public string ItemType { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public DateTime Added { get; set; }
    public DateTime Updated { get; set; }
}