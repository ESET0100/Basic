using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
       
        private static readonly List<Dictionary<string, object>> Students = new List<Dictionary<string, object>>
        {
            new Dictionary<string, object>
            {
                { "id", 1 },
                { "name", "Shivansh" },
                { "age", 22 },
                { "department", "Computer Science" },
                { "email", "Shivansh@email.com" }
            },
            new Dictionary<string, object>
            {
                { "id", 2 },
                { "name", "Lakshay saxsena" },
                { "age", 21 },
                { "department", "Mathematics" },
                { "email", "LKS@email.com" }
            },
            new Dictionary<string, object>
            {
                { "id", 3 },
                { "name", "VEER " },
                { "age", 22 },
                { "department", "Physics" },
                { "email", "V@email.com" }
            },
            new Dictionary<string, object>
            {
                { "id", 4 },
                { "name", "Piyush" },
                { "age", 19 },
                { "department", "Chemistry" },
                { "email", "PI@email.com" }
            },
            new Dictionary<string, object>
            {
                { "id", 5 },
                { "name", "Surya" },
                { "age", 23 },
                { "department", "Biology" },
                { "email", "Surya@email.com" }
            }
        };

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            return Ok(new
            {
                message = "Students retrieved successfully",
                count = Students.Count,
                students = Students
            });
        }

        
        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student = Students.FirstOrDefault(s => (int)s["id"] == id);
            if (student == null)
            {
                return NotFound(new { message = $"Student with ID {id} not found" });
            }
            return Ok(new { message = "Student found", student });
        }

        
        [HttpGet("search")]
        public IActionResult SearchStudents([FromQuery] int? id, [FromQuery] string? name)
        {
           
            if (id == null && string.IsNullOrEmpty(name))
            {
                return Ok(new
                {
                    message = "All students retrieved",
                    count = Students.Count,
                    students = Students
                });
            }

            var filteredStudents = Students.AsEnumerable();

            if (id.HasValue)
            {
                filteredStudents = filteredStudents.Where(s => (int)s["id"] == id.Value);
            }

            if (!string.IsNullOrEmpty(name))
            {
                filteredStudents = filteredStudents.Where(s =>
                    ((string)s["name"]).Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            var result = filteredStudents.ToList();

            if (result.Count == 0)
            {
                return NotFound(new { message = "No students found matching the criteria" });
            }

            return Ok(new
            {
                message = "Students found matching criteria",
                count = result.Count,
                students = result
            });
        }

        
        [HttpGet("filter")]
        public IActionResult FilterStudents([FromQuery] int? id, [FromQuery] string? name)
        {
            
            if (id == null && string.IsNullOrEmpty(name))
            {
                return BadRequest(new { message = "Please provide at least one filter parameter (id or name)" });
            }

            var filteredStudents = Students.AsEnumerable();

            if (id.HasValue)
            {
                filteredStudents = filteredStudents.Where(s => (int)s["id"] == id.Value);
            }

            if (!string.IsNullOrEmpty(name))
            {
                filteredStudents = filteredStudents.Where(s =>
                    ((string)s["name"]).Equals(name, StringComparison.OrdinalIgnoreCase));
            }

            var result = filteredStudents.ToList();

            if (result.Count == 0)
            {
                return NotFound(new { message = "No students found matching the exact criteria" });
            }

            return Ok(new
            {
                message = "Students found with exact match",
                count = result.Count,
                students = result
            });
        }

        
        [HttpGet("count")]
        public IActionResult GetStudentCount()
        {
            return Ok(new { totalStudents = Students.Count });
        }

        
        private Dictionary<string, object>? FindStudentById(int id)
        {
            return Students.FirstOrDefault(s => (int)s["id"] == id);
        }

        
        [HttpGet("exists/{id}")]
        public IActionResult CheckStudentExists(int id)
        {
            var exists = Students.Any(s => (int)s["id"] == id);
            return Ok(new { studentId = id, exists });
        }
    }
}