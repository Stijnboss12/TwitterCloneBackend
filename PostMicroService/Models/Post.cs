using System.ComponentModel.DataAnnotations;

namespace PostMicroService.Models
{
    public class Post
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
