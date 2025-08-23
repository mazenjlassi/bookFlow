namespace bookFlow.Dto
{
    public class CreateBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public string PublishedDate { get; set; }  // string if your DB is string
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
