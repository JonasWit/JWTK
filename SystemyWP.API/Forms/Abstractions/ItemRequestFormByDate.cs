using System;

namespace SystemyWP.API.Forms.Abstractions
{
    public class ItemRequestFormByDate<TKey>: ItemRequestFormBase<TKey>
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}