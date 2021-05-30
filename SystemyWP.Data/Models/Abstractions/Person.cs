namespace SystemyWP.Data.Models.Abstractions
{
    public class Person<TKey> : TrackedModel<TKey>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}