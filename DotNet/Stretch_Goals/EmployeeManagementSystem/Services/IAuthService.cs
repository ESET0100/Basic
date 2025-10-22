using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Models.DTOs;

namespace EmployeeManagementSystem.Services
{
    public interface IAuthService
    {
        Task<User> Register(RegisterDto registerDto);
        Task<string> Login(LoginDto loginDto);
        Task<bool> UserExists(string username);
    }
}