using System.ComponentModel.DataAnnotations;

namespace NewsSite.Models
{
    public class NewsModel
    {
        public int Id { get; set; }

        [Required]
        public string Header { get; set; }

        [Required]
        public string Subheader { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
    }
}