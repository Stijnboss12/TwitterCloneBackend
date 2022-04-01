using System.ComponentModel.DataAnnotations;

namespace UserMicroService.Models
{
    public class User
    {
        [Required]
        public string Id { get; set; } = string.Empty;
        [Required]
        public string Username { get; set; } = string.Empty;
    }
}
