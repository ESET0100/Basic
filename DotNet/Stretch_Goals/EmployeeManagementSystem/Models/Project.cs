using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace EmployeeManagementSystem.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(100)]
        public string ProjectName { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Budget { get; set; }

        // JSON array for team members
        public string TeamMembersJSON { get; set; } = "[]";

        [StringLength(50)]
        public string Status { get; set; } = "Planning";

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual Department? Department { get; set; }

        // Methods to handle JSON field
        [NotMapped]
        public List<int> TeamMembers
        {
            get => JsonSerializer.Deserialize<List<int>>(string.IsNullOrEmpty(TeamMembersJSON) ? "[]" : TeamMembersJSON) ?? new List<int>();
            set => TeamMembersJSON = JsonSerializer.Serialize(value);
        }
    }
}