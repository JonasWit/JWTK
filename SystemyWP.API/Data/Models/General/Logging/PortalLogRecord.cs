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
        
        [MaxLength(500)]
        public string Description { get; set; }
        
        [MaxLength(500)]
        public string Endpoint { get; set; } 
        
        public string ExceptionMessage { get; set; } 
        
        public string ExceptionStackTrace { get; set; }
        
        [MaxLength(70)]
        [EmailAddress]
        public string UserEmail { get; set; }

        [MaxLength(512)]
        public string UserId { get; set; }
    }
}