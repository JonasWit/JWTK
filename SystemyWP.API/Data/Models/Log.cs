using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemyWP.API.Data.Models;

public class Log
{
    public string Message { get; set; }
    
    public string MessageTemplate { get; set; }
    
    public string Level { get; set; }
    
    public DateTime RaiseDate { get; set; }
    
    public string Exception { get; set; }
  
    [Column(TypeName = "jsonb")]
    public string Properties { get; set; }
}