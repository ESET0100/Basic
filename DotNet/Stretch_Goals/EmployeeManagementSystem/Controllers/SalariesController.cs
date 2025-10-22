using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SalariesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SalariesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetAllSalaries()
        {
            var salaries = await _context.Salaries
                .Include(s => s.Employee)
                .Select(s => new
                {
                    s.SalaryId,
                    s.EmployeeId,
                    EmployeeName = $"{s.Employee.FirstName} {s.Employee.LastName}",
                    s.BasicSalary,
                    s.Allowances,
                    s.Deductions,
                    TotalSalary = s.TotalSalary,
                    s.EffectiveDate,
                    s.CreatedDate
                })
                .ToListAsync();

            return Ok(salaries);
        }

        [HttpGet("employee/{employeeId}")]
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> GetSalariesByEmployee(int employeeId)
        {
            var salaries = await _context.Salaries
                .Where(s => s.EmployeeId == employeeId)
                .Include(s => s.Employee)
                .Select(s => new
                {
                    s.SalaryId,
                    s.EmployeeId,
                    EmployeeName = $"{s.Employee.FirstName} {s.Employee.LastName}",
                    s.BasicSalary,
                    s.Allowances,
                    s.Deductions,
                    TotalSalary = s.TotalSalary,
                    s.EffectiveDate,
                    s.CreatedDate
                })
                .OrderByDescending(s => s.EffectiveDate)
                .ToListAsync();

            return Ok(salaries);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetSalaryById(int id)
        {
            var salary = await _context.Salaries
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(s => s.SalaryId == id);

            if (salary == null)
                return NotFound();

            var result = new
            {
                salary.SalaryId,
                salary.EmployeeId,
                Employee = new
                {
                    salary.Employee.EmployeeId,
                    Name = $"{salary.Employee.FirstName} {salary.Employee.LastName}",
                    salary.Employee.Email,
                    Department = salary.Employee.Department != null ? salary.Employee.Department.DepartmentName : "N/A"
                },
                salary.BasicSalary,
                salary.Allowances,
                salary.Deductions,
                TotalSalary = salary.TotalSalary,
                salary.EffectiveDate,
                salary.CreatedDate
            };

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> CreateSalary(Salary salary)
        {
            try
            {
                salary.CreatedDate = DateTime.UtcNow;
                _context.Salaries.Add(salary);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetSalaryById), new { id = salary.SalaryId }, salary);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating salary record: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateSalary(int id, Salary salary)
        {
            if (id != salary.SalaryId)
                return BadRequest();

            try
            {
                _context.Entry(salary).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalaryExists(id))
                    return NotFound();
                throw;
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSalary(int id)
        {
            var salary = await _context.Salaries.FindAsync(id);
            if (salary == null)
                return NotFound();

            _context.Salaries.Remove(salary);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("department/{departmentId}/summary")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetDepartmentSalarySummary(int departmentId)
        {
            // Advanced SQL with CTE-like operations using LINQ
            var summary = await _context.Salaries
                .Where(s => s.Employee.DepartmentId == departmentId)
                .GroupBy(s => s.Employee.DepartmentId)
                .Select(g => new
                {
                    DepartmentId = g.Key,
                    DepartmentName = g.First().Employee.Department.DepartmentName,
                    TotalEmployees = g.Select(s => s.EmployeeId).Distinct().Count(),
                    AverageBasicSalary = g.Average(s => s.BasicSalary),
                    AverageTotalSalary = g.Average(s => s.TotalSalary),
                    TotalMonthlySalary = g.Sum(s => s.TotalSalary),
                    MaxSalary = g.Max(s => s.TotalSalary),
                    MinSalary = g.Min(s => s.TotalSalary)
                })
                .FirstOrDefaultAsync();

            if (summary == null)
                return NotFound("No salary data found for this department");

            return Ok(summary);
        }

        [HttpGet("salary-distribution")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetSalaryDistribution()
        {
            // Using CASE statements and grouping for salary distribution
            var distribution = await _context.Salaries
                .Include(s => s.Employee)
                .ThenInclude(e => e.Department)
                .GroupBy(s => s.Employee.DepartmentId)
                .Select(g => new
                {
                    DepartmentId = g.Key,
                    DepartmentName = g.First().Employee.Department.DepartmentName,
                    SalaryRanges = new
                    {
                        Low = g.Count(s => s.TotalSalary < 50000),
                        Medium = g.Count(s => s.TotalSalary >= 50000 && s.TotalSalary < 100000),
                        High = g.Count(s => s.TotalSalary >= 100000)
                    },
                    AverageSalary = g.Average(s => s.TotalSalary),
                    TotalEmployees = g.Select(s => s.EmployeeId).Distinct().Count()
                })
                .ToListAsync();

            return Ok(distribution);
        }

        [HttpPost("bulk-update-allowances")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BulkUpdateAllowances([FromBody] BulkAllowanceUpdateDto updateDto)
        {
            try
            {
                var currentSalaries = await _context.Salaries
                    .Where(s => s.EffectiveDate <= DateTime.UtcNow)
                    .GroupBy(s => s.EmployeeId)
                    .Select(g => g.OrderByDescending(s => s.EffectiveDate).First())
                    .ToListAsync();

                foreach (var salary in currentSalaries)
                {
                    var allowances = salary.Allowances;
                    if (allowances.ContainsKey(updateDto.AllowanceName))
                    {
                        allowances[updateDto.AllowanceName] = updateDto.NewAmount;
                    }
                    else
                    {
                        allowances.Add(updateDto.AllowanceName, updateDto.NewAmount);
                    }
                    salary.Allowances = allowances;
                }

                await _context.SaveChangesAsync();
                return Ok(new { message = "Allowances updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating allowances: {ex.Message}");
            }
        }

        private bool SalaryExists(int id)
        {
            return _context.Salaries.Any(e => e.SalaryId == id);
        }
    }

    public class BulkAllowanceUpdateDto
    {
        public string AllowanceName { get; set; } = string.Empty;
        public decimal NewAmount { get; set; }
    }
}