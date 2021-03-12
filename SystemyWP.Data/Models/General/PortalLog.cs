using SystemyWP.Data.Enums;
using SystemyWP.Data.Models.Abstractions;

namespace SystemyWP.Data.Models.General
{
    public class PortalLog : BaseModel<long>
    {
        public LogType LogType { get; set; }
        public string SourceType { get; set; }
        public string SourceMethod { get; set; }
        public string Message { get; set; }     
        public string ExceptionDetails { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }     
    }
}