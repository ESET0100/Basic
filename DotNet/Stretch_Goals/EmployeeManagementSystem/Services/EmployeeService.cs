using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Models.DTOs;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployees()
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Select(e => new EmployeeDto
                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    Phone = e.Phone,
                    DepartmentId = e.DepartmentId,
                    Salary = e.Salary,
                    Skills = e.Skills,
                    Address = e.Address,
                    CreatedDate = e.CreatedDate,
                    IsActive = e.IsActive,
                    DepartmentName = e.Department!.DepartmentName
                })
                .ToListAsync();
        }

        public async Task<EmployeeDto?> GetEmployeeById(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null) return null;

            return new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Phone = employee.Phone,
                DepartmentId = employee.DepartmentId,
                Salary = employee.Salary,
                Skills = employee.Skills,
                Address = employee.Address,
                CreatedDate = employee.CreatedDate,
                IsActive = employee.IsActive,
                DepartmentName = employee.Department?.DepartmentName
            };
        }

        public async Task<Employee> CreateEmployee(EmployeeDto employeeDto)
        {
            var employee = new Employee
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Email = employeeDto.Email,
                Phone = employeeDto.Phone,
                DepartmentId = employeeDto.DepartmentId,
                Salary = employeeDto.Salary,
                Skills = employeeDto.Skills,
                Address = employeeDto.Address
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> UpdateEmployee(int id, EmployeeDto employeeDto)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return null;

            employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;
            employee.Email = employeeDto.Email;
            employee.Phone = employeeDto.Phone;
            employee.DepartmentId = employeeDto.DepartmentId;
            employee.Salary = employeeDto.Salary;
            employee.Skills = employeeDto.Skills;
            employee.Address = employeeDto.Address;
            employee.IsActive = employeeDto.IsActive;

            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByDepartment(int departmentId)
        {
            return await _context.Employees
                .Where(e => e.DepartmentId == departmentId)
                .Include(e => e.Department)
                .Select(e => new EmployeeDto
                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    Phone = e.Phone,
                    DepartmentId = e.DepartmentId,
                    Salary = e.Salary,
                    Skills = e.Skills,
                    Address = e.Address,
                    CreatedDate = e.CreatedDate,
                    IsActive = e.IsActive,
                    DepartmentName = e.Department!.DepartmentName
                })
                .ToListAsync();
        }

        public async Task<object> GetEmployeeSalaryAnalysis(int employeeId)
        {
            // Using CTE and advanced SQL features
            var analysis = await _context.Salaries
                .Where(s => s.EmployeeId == employeeId)
                .OrderByDescending(s => s.EffectiveDate)
                .Select(s => new
                {
                    s.SalaryId,
                    s.BasicSalary,
                    s.Allowances,
                    s.Deductions,
                    s.TotalSalary,
                    s.EffectiveDate
                })
                .ToListAsync();

            return new
            {
                CurrentSalary = analysis.FirstOrDefault(),
                SalaryHistory = analysis,
                AverageSalary = analysis.Average(s => s.TotalSalary),
                MaxSalary = analysis.Max(s => s.TotalSalary),
                MinSalary = analysis.Min(s => s.TotalSalary)
            };
        }
    }
}