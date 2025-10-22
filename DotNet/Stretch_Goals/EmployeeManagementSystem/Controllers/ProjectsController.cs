using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _context.Projects
                .Include(p => p.Department)
                .Select(p => new
                {
                    p.ProjectId,
                    p.ProjectName,
                    p.Description,
                    p.StartDate,
                    p.EndDate,
                    p.Budget,
                    p.Status,
                    p.CreatedDate,
                    DepartmentName = p.Department.DepartmentName,
                    TeamMemberCount = p.TeamMembers.Count
                })
                .ToListAsync();

            return Ok(projects);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _context.Projects
                .Include(p => p.Department)
                .FirstOrDefaultAsync(p => p.ProjectId == id);

            if (project == null)
                return NotFound();

            // Get team member details using JSON parsing
            var teamMembers = await _context.Employees
                .Where(e => project.TeamMembers.Contains(e.EmployeeId))
                .Select(e => new
                {
                    e.EmployeeId,
                    Name = $"{e.FirstName} {e.LastName}",
                    e.Email,
                    e.DepartmentId
                })
                .ToListAsync();

            var result = new
            {
                project.ProjectId,
                project.ProjectName,
                project.Description,
                project.StartDate,
                project.EndDate,
                project.Budget,
                project.Status,
                project.CreatedDate,
                Department = new
                {
                    project.Department.DepartmentId,
                    project.Department.DepartmentName
                },
                TeamMembers = teamMembers
            };

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> CreateProject(Project project)
        {
            try
            {
                project.CreatedDate = DateTime.UtcNow;
                _context.Projects.Add(project);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProjectById), new { id = project.ProjectId }, project);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating project: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateProject(int id, Project project)
        {
            if (id != project.ProjectId)
                return BadRequest();

            try
            {
                _context.Entry(project).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
                    return NotFound();
                throw;
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return NotFound();

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id}/add-team-member/{employeeId}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> AddTeamMember(int id, int employeeId)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return NotFound("Project not found");

            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee == null)
                return NotFound("Employee not found");

            try
            {
                var teamMembers = project.TeamMembers;
                if (!teamMembers.Contains(employeeId))
                {
                    teamMembers.Add(employeeId);
                    project.TeamMembers = teamMembers;
                    await _context.SaveChangesAsync();
                }

                return Ok(new { message = "Team member added successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding team member: {ex.Message}");
            }
        }

        [HttpPost("{id}/remove-team-member/{employeeId}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> RemoveTeamMember(int id, int employeeId)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return NotFound("Project not found");

            try
            {
                var teamMembers = project.TeamMembers;
                if (teamMembers.Contains(employeeId))
                {
                    teamMembers.Remove(employeeId);
                    project.TeamMembers = teamMembers;
                    await _context.SaveChangesAsync();
                }

                return Ok(new { message = "Team member removed successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error removing team member: {ex.Message}");
            }
        }

        [HttpGet("department/{departmentId}")]
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> GetProjectsByDepartment(int departmentId)
        {
            var projects = await _context.Projects
                .Where(p => p.DepartmentId == departmentId)
                .Include(p => p.Department)
                .Select(p => new
                {
                    p.ProjectId,
                    p.ProjectName,
                    p.Description,
                    p.StartDate,
                    p.EndDate,
                    p.Budget,
                    p.Status,
                    TeamMemberCount = p.TeamMembers.Count
                })
                .ToListAsync();

            return Ok(projects);
        }

        [HttpGet("status-report")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetProjectStatusReport()
        {
            // Using GROUP BY and conditional logic for status report
            var statusReport = await _context.Projects
                .GroupBy(p => p.Status)
                .Select(g => new
                {
                    Status = g.Key,
                    Count = g.Count(),
                    TotalBudget = g.Sum(p => p.Budget),
                    AverageBudget = g.Average(p => p.Budget),
                    Projects = g.Select(p => new
                    {
                        p.ProjectId,
                        p.ProjectName,
                        p.StartDate,
                        p.EndDate
                    })
                })
                .ToListAsync();

            return Ok(statusReport);
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.ProjectId == id);
        }
    }
}