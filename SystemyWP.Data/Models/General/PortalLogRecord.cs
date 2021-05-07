using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Enums;
using SystemyWP.Data.Models.Abstractions;

namespace SystemyWP.Data.Models.General
{
    public class PortalLogRecord : BaseModel<long>
    {
        [Required]
        public LogType LogType { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [MaxLength(500)]
        public string Endpoint { get; set; } 
        public string ExceptionMessage { get; set; }   
        public string ExceptionStackTrace { get; set; }
        [MaxLength(70)]
        [Required]
        public string UserEmail { get; set; }
    }
}