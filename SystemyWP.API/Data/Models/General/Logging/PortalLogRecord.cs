using System.ComponentModel.DataAnnotations;
using SystemyWP.API.Data.Enums;

namespace SystemyWP.API.Data.Models.General.Logging
{
    public class PortalLogRecord
    {
        [Key]
        [Required]
        public long Id { get; set; }
        
        [Required]
        public LogType LogType { get; set; }
        
        public string Description { get; set; }
        
        [MaxLength(500)]
        public string Endpoint { get; set; }

        public string ExceptionStackTrace { get; set; }
        
        [MaxLength(128)]
        [EmailAddress]
        public string UserEmail { get; set; }
    }
}