using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DepartmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DepartmentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _context.Departments
                .Include(d => d.Manager)
                .Select(d => new
                {
                    d.DepartmentId,
                    d.DepartmentName,
                    d.Description,
                    d.Budget,
                    d.CreatedDate,
                    ManagerName = d.Manager != null ? $"{d.Manager.FirstName} {d.Manager.LastName}" : "Not Assigned",
                    EmployeeCount = d.Employees != null ? d.Employees.Count : 0
                })
                .ToListAsync();

            return Ok(departments);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var department = await _context.Departments
                .Include(d => d.Manager)
                .Include(d => d.Employees)
                .FirstOrDefaultAsync(d => d.DepartmentId == id);

            if (department == null)
                return NotFound();

            // FIXED: Create employee list without type conversion issues
            var employeeList = new List<object>();
            if (department.Employees != null && department.Employees.Any())
            {
                employeeList = department.Employees.Select(e => new
                {
                    e.EmployeeId,
                    Name = $"{e.FirstName} {e.LastName}",
                    e.Email,
                    e.Salary
                }).ToList<object>();
            }

            var result = new
            {
                department.DepartmentId,
                department.DepartmentName,
                department.Description,
                department.Budget,
                department.CreatedDate,
                Manager = department.Manager != null ? new
                {
                    department.Manager.EmployeeId,
                    Name = $"{department.Manager.FirstName} {department.Manager.LastName}",
                    department.Manager.Email
                } : null,
                Employees = employeeList
            };

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentCreateDto departmentDto)
        {
            try
            {
                var department = new Department
                {
                    DepartmentName = departmentDto.DepartmentName,
                    Description = departmentDto.Description,
                    ManagerId = departmentDto.ManagerId,
                    Budget = departmentDto.Budget,
                    CreatedDate = DateTime.UtcNow
                };

                _context.Departments.Add(department);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetDepartmentById), new { id = department.DepartmentId }, new
                {
                    department.DepartmentId,
                    department.DepartmentName,
                    department.Description,
                    department.Budget,
                    department.CreatedDate
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating department: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentUpdateDto departmentDto)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
                return NotFound();

            try
            {
                department.DepartmentName = departmentDto.DepartmentName;
                department.Description = departmentDto.Description;
                department.ManagerId = departmentDto.ManagerId;
                department.Budget = departmentDto.Budget;

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
                    return NotFound();
                throw;
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating department: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
                return NotFound();

            // Check if department has employees
            var hasEmployees = await _context.Employees.AnyAsync(e => e.DepartmentId == id);
            if (hasEmployees)
                return BadRequest("Cannot delete department with existing employees");

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}/budget-analysis")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetDepartmentBudgetAnalysis(int id)
        {
            var analysis = await _context.Departments
                .Where(d => d.DepartmentId == id)
                .Select(d => new
                {
                    Department = d.DepartmentName,
                    TotalBudget = d.Budget,
                    TotalSalaries = d.Employees != null ? d.Employees.Sum(e => e.Salary) : 0,
                    RemainingBudget = d.Budget - (d.Employees != null ? d.Employees.Sum(e => e.Salary) : 0),
                    BudgetUtilization = d.Budget > 0 ?
                        ((d.Employees != null ? d.Employees.Sum(e => e.Salary) : 0) / d.Budget) * 100 : 0,
                    EmployeeCount = d.Employees != null ? d.Employees.Count : 0,
                    AverageSalary = d.Employees != null && d.Employees.Count > 0 ?
                        d.Employees.Average(e => e.Salary) : 0
                })
                .FirstOrDefaultAsync();

            if (analysis == null)
                return NotFound();

            return Ok(analysis);
        }

        [HttpGet("{id}/employees")]
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> GetDepartmentEmployees(int id)
        {
            var employees = await _context.Employees
                .Where(e => e.DepartmentId == id && e.IsActive)
                .Select(e => new
                {
                    e.EmployeeId,
                    Name = $"{e.FirstName} {e.LastName}",
                    e.Email,
                    e.Phone,
                    e.Salary,
                    e.IsActive,
                    Skills = e.Skills,
                    Address = e.Address
                })
                .ToListAsync();

            return Ok(employees);
        }

        [HttpPost("{id}/assign-manager/{managerId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignManager(int id, int managerId)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
                return NotFound("Department not found");

            var manager = await _context.Employees.FindAsync(managerId);
            if (manager == null)
                return NotFound("Employee not found");

            // Check if manager belongs to the same department
            if (manager.DepartmentId != id)
                return BadRequest("Manager must belong to the same department");

            department.ManagerId = managerId;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Manager assigned successfully" });
        }

        [HttpGet("stats/summary")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetDepartmentStatsSummary()
        {
            var stats = await _context.Departments
                .Include(d => d.Employees)
                .Select(d => new
                {
                    d.DepartmentId,
                    d.DepartmentName,
                    EmployeeCount = d.Employees.Count(e => e.IsActive),
                    TotalSalary = d.Employees.Where(e => e.IsActive).Sum(e => e.Salary),
                    AverageSalary = d.Employees.Where(e => e.IsActive).Average(e => e.Salary),
                    BudgetUtilization = d.Budget > 0 ? (d.Employees.Where(e => e.IsActive).Sum(e => e.Salary) / d.Budget) * 100 : 0
                })
                .ToListAsync();

            return Ok(stats);
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.DepartmentId == id);
        }
    }

    // DTOs for Department operations
    public class DepartmentCreateDto
    {
        public string DepartmentName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? ManagerId { get; set; }
        public decimal Budget { get; set; }
    }

    public class DepartmentUpdateDto
    {
        public string DepartmentName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? ManagerId { get; set; }
        public decimal Budget { get; set; }
    }
}