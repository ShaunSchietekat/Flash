using System.ComponentModel.DataAnnotations;

namespace Sanitize.Models
{
    public class WordModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Word { get; set; }
    }
}
