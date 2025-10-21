using DbWebApp.Data;
using DbWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DbWebApp.Services
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthService(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User?> RegisterUser(string username, string email, string password)
        {
            // Check if user already exists
            if (_context.Users.Any(u => u.Username == username || u.Email == email))
            {
                return null;
            }

            // In a real app, use proper password hashing like BCrypt
            var passwordHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));

            var user = new User
            {
                Username = username,
                Email = email,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> AuthenticateUser(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return null;

            // In a real app, use proper password verification
            var inputPasswordHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));

            if (user.PasswordHash != inputPasswordHash)
                return null;

            return user;
        }
    }
}