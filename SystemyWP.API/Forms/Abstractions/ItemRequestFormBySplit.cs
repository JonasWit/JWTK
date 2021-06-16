namespace SystemyWP.API.Forms.Abstractions
{
    public class ItemRequestFormBySplit<TKey> : ItemRequestFormBase<TKey>
    {
        public long Take { get; set; }
        public long Cursor { get; set; }
    }
}