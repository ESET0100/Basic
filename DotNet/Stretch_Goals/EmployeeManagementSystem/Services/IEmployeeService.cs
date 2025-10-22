using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Models.DTOs;

namespace EmployeeManagementSystem.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployees();
        Task<EmployeeDto?> GetEmployeeById(int id);
        Task<Employee> CreateEmployee(EmployeeDto employeeDto);
        Task<Employee?> UpdateEmployee(int id, EmployeeDto employeeDto);
        Task<bool> DeleteEmployee(int id);
        Task<IEnumerable<EmployeeDto>> GetEmployeesByDepartment(int departmentId);
        Task<object> GetEmployeeSalaryAnalysis(int employeeId);
    }
}