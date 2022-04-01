using System.ComponentModel.DataAnnotations;

namespace UserMicroService.Models.DTO
{
    public class UserDTO
    {
        [Required]
        public string Id { get; set; } = string.Empty;
        [Required]
        public string Username { get; set; } = string.Empty;
    }
}
