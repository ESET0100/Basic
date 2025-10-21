using Microsoft.AspNetCore.Mvc;
using DbWebApp.Services;
using DbWebApp.DTOs;

namespace DbWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var user = await _authService.RegisterUser(request.Username, request.Email, request.Password);

            if (user == null)
                return BadRequest("Username or email already exists");

            var token = _authService.GenerateJwtToken(user);

            return Ok(new
            {
                Message = "Registration successful",
                Token = token,
                UserId = user.UserId,
                Username = user.Username
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _authService.AuthenticateUser(request.Username, request.Password);

            if (user == null)
                return Unauthorized("Invalid username or password");

            var token = _authService.GenerateJwtToken(user);

            return Ok(new
            {
                Message = "Login successful",
                Token = token,
                UserId = user.UserId,
                Username = user.Username
            });
        }
    }
}