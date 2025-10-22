using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace EmployeeManagementSystem.Models
{
    public class Salary
    {
        [Key]
        public int SalaryId { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal BasicSalary { get; set; }

        // JSON field for allowances
        public string AllowancesJSON { get; set; } = "{}";

        // JSON field for deductions
        public string DeductionsJSON { get; set; } = "{}";

        public DateTime EffectiveDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual Employee? Employee { get; set; }

        // Methods to handle JSON fields
        [NotMapped]
        public Dictionary<string, decimal> Allowances
        {
            get => JsonSerializer.Deserialize<Dictionary<string, decimal>>(string.IsNullOrEmpty(AllowancesJSON) ? "{}" : AllowancesJSON) ?? new Dictionary<string, decimal>();
            set => AllowancesJSON = JsonSerializer.Serialize(value);
        }

        [NotMapped]
        public Dictionary<string, decimal> Deductions
        {
            get => JsonSerializer.Deserialize<Dictionary<string, decimal>>(string.IsNullOrEmpty(DeductionsJSON) ? "{}" : DeductionsJSON) ?? new Dictionary<string, decimal>();
            set => DeductionsJSON = JsonSerializer.Serialize(value);
        }

        [NotMapped]
        public decimal TotalSalary => BasicSalary + Allowances.Values.Sum() - Deductions.Values.Sum();
    }
}