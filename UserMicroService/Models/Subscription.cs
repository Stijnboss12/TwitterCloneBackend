using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserMicroService.Models
{
    public class Subscription
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string SubscriberId { get; set; } = string.Empty;
        [Required]
        public string SubscribedId { get; set; } = string.Empty;
    }
}
