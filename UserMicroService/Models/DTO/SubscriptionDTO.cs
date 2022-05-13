using System.ComponentModel.DataAnnotations;

namespace UserMicroService.Models.DTO
{
    public class SubscriptionDTO
    {
        public Guid Id { get; set; }
        public string SubscriberId { get; set; } = string.Empty;
        [Required]
        public string SubscribedId { get; set; } = string.Empty;
    }
}
