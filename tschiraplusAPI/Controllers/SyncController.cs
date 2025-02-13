using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tschiraplusAPI.Data;

namespace tschiraplusAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SyncController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SyncController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // GET: api/sync/projects/{userId}
    [HttpGet("projects/{userId:guid}")]
    public async Task<ActionResult<object>> SyncProjects(Guid userId)
    {
        try
        {
            // load user information
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            // load all ProjectUserModels for given userId
            var projectUsers = await _context.ProjectUsers.Where(pu => pu.UserId == userId).ToListAsync();
            if (projectUsers.Count == 0)
            {
                return NotFound(new { message = "No projects found for this user." });
            }

            // load all projects from the found ProjectUserModels
            var projectIds = projectUsers.Select(p => p.ProjectId).Distinct();
            var projects = await _context.Projects.Where(p => projectIds.Contains(p.ProjectId)).ToListAsync();

            return Ok(new
            {
                User = user,
                ProjectUsers = projectUsers,
                Projects = projects
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new { message = "An error occured while syncing projects.", details = e.Message });
        }
    }
    
    // GET: api/sync/tasks/{projectId}
    [HttpGet("tasks/{projectId:guid}")]
    public async Task<ActionResult<object>> SyncTasks(Guid projectId)
    {
        try
        {
            var tasks = await _context.Tasks.Where(t => t.ProjectId == projectId).ToListAsync();
            return Ok(tasks);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new { error = "An error occurred while syncing tasks.", details = e.Message });
        }
    }
}