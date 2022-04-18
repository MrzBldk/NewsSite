namespace NewsSite.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Subheader { get; set; }
        public string Text { get; set; }
        public byte[] Image { get; set; }
    }
}