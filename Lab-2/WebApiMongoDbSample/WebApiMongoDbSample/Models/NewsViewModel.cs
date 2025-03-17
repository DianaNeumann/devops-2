using System.ComponentModel.DataAnnotations;

namespace WebApiMongoDbSample.Models
{
    public class NewsViewModel
    {
        [Required]
        public string Body { get; set; }

        [Required]
        public string PhotoUrl { get; set; }
    }
}
