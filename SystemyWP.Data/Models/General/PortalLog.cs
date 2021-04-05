using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Enums;
using SystemyWP.Data.Models.Abstractions;

namespace SystemyWP.Data.Models.General
{
    public class PortalLog : BaseModel<long>
    {
        [Required]
        public LogType LogType { get; set; }
        public string Description { get; set; }
        public string Endpoint { get; set; } 
        public string ExceptionMessage { get; set; }   
        public string ExceptionStackTrace { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string UserEmail { get; set; }
    }
}