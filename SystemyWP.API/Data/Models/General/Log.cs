using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Nodes;

namespace SystemyWP.API.Data.Models.General;

public class Log
{
    public int Id { get; set; }
    
    public string Message { get; set; }
    
    public string MessageTemplate { get; set; }
    
    public string Level { get; set; }
    
    public DateTime TimeStamp { get; set; }
    
    public string Exception { get; set; }
    
    [Column(TypeName = "jsonb")]
    public string LogEvent { get; set; }
}