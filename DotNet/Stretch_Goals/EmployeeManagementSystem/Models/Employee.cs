using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Phone { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }

        // JSON field for skills
        public string SkillsJSON { get; set; } = "[]";

        // JSON field for address
        public string AddressJSON { get; set; } = "{}";

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual Department? Department { get; set; }
        public virtual ICollection<Salary> Salaries { get; set; } = new List<Salary>();

        // Methods to handle JSON fields
        [NotMapped]
        public List<string> Skills
        {
            get => JsonSerializer.Deserialize<List<string>>(string.IsNullOrEmpty(SkillsJSON) ? "[]" : SkillsJSON) ?? new List<string>();
            set => SkillsJSON = JsonSerializer.Serialize(value);
        }

        [NotMapped]
        public Dictionary<string, string> Address
        {
            get => JsonSerializer.Deserialize<Dictionary<string, string>>(string.IsNullOrEmpty(AddressJSON) ? "{}" : AddressJSON) ?? new Dictionary<string, string>();
            set => AddressJSON = JsonSerializer.Serialize(value);
        }
    }
}