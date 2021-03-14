using System;

namespace SystemyWP.Data.Models.Abstractions
{
    public abstract class BaseModel<TKey>
    {
        public TKey Id { get; set; }
        
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        
        public string UpdatedBy { get; set; }
        public DateTime Updated { get; set; } = DateTime.UtcNow;
    }
}