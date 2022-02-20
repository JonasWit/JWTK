using System.ComponentModel.DataAnnotations;

namespace SystemyWP.API.Gastronomy.Data.Models.BaseClasses;

public class BaseModel
{
    [Key]
    public long Id { get; set; }
    
    [Required]
    [MaxLength(256)]
    public string AccessKey { get; set; }
}