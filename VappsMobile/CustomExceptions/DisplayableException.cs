namespace VappsMobile.CustomExceptions
{
    public class DisplayableException : Exception
    {
        public DisplayableException(string message) : base(message) { }
        public DisplayableException(string message, Exception inner) : base(message) { }
    }
}
