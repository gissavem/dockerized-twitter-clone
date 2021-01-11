using System.ComponentModel.DataAnnotations;

namespace TwitterClone.Database.DTOs
{
    public class PostMessageDTO
    {
        [Required]
        [StringLength(300, ErrorMessage = "The content value cannot exceed 300 characters. ")]
        public string Content { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "The author value cannot exceed 25 characters. ")]
        public string Author { get; set; }
        
    }
}