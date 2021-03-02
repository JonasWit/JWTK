using System;

namespace SystemyWP.Data.Models.Abstractions
{
    public abstract class BaseModel<TKey>
    {
        public TKey Id { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}