namespace SimpleDotNetWebApiApp
{
    public class AttachmentOptions
    {
        public int MaxSize { get; set; }
        public string AllowedTypes { get; set; }
        public bool EnableCompression { get; set; }
    }
}
