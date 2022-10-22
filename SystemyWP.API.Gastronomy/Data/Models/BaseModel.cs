using System.ComponentModel.DataAnnotations;

namespace MasterService.API.Gastronomy.Data.Models;

public abstract record BaseModel
{
    [Key] public long Id { get; set; }

    [Required]
    [MaxLength(AppConstants.DataLimits.KeyLimit)]
    public string AccessKey { get; set; }

    [MaxLength(AppConstants.DataLimits.NameLimit)]
    public string Category { get; set; }
}