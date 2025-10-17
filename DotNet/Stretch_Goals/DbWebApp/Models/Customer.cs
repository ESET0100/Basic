using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DbWebApp.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Address { get; set; } = string.Empty;

        // Navigation property
        [JsonIgnore]
        public virtual ICollection<Order>? Orders { get; set; }
    }
}