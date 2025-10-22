using System.Text.Json;

namespace EmployeeManagementSystem.Models.DTOs
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public int DepartmentId { get; set; }
        public decimal Salary { get; set; }
        public List<string> Skills { get; set; } = new List<string>();
        public Dictionary<string, string> Address { get; set; } = new Dictionary<string, string>();
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public string? DepartmentName { get; set; }
    }
}