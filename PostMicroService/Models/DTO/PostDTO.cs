using System.ComponentModel.DataAnnotations;

namespace PostMicroService.Models.DTO
{
    public class PostDTO
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        [Required]
        [MaxLength(300)]
        public string Content { get; set; } = string.Empty;
    }
}
