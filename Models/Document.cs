using System.ComponentModel.DataAnnotations;

namespace NewTestProject.Models
{
    public class Document
    {
        [Key]
        public string? Name { get; set; }

        public IFormFile? File { get; set; }
    }
}
