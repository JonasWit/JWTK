namespace VappsMobile.Models.GeneralModels
{
    public class DefaultModalContent
    {
        public string Icon { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public Func<Task> OnClickAction { get; set; }
    }
}
