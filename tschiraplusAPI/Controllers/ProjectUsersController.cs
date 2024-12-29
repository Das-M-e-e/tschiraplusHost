using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tschiraplusAPI.Data;
using tschiraplusAPI.Models;

namespace tschiraplusAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectUsersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProjectUsersController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // GET: api/ProjectUsers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectUserModel>>> GetProjectUsers()
    {
        return await _context.ProjectUsers.ToListAsync();
    }
    
    // GET: api/ProjectUsers/ByUserId/{userId}
    [HttpGet("ByUserId/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<ProjectUserModel>>> GetProjectUsersByUserId(Guid userId)
    {
        var projectUsers = await _context.ProjectUsers.Where(p => p.UserId == userId).ToListAsync();

        if (projectUsers.Count == 0)
        {
            return NotFound(new { Message = "No projectUsers found for given UserId." });
        }
        
        return Ok(projectUsers);
    }
    
    // GET: api/ProjectUsers/ByProjectId/{projectId}
    [HttpGet("ByProjectId/{projectId:guid}")]
    public async Task<ActionResult<IEnumerable<ProjectUserModel>>> GetProjectUsersByProjectId(Guid projectId)
    {
        var projectUsers = await _context.ProjectUsers.Where(p => p.ProjectId == projectId).ToListAsync();

        if (projectUsers.Count == 0)
        {
            return NotFound(new { Message = "No projectUsers found for given ProjectId." });
        }
        
        return Ok(projectUsers);
    }
    
    // GET: api/ProjectUsers/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProjectUserModel>> GetProjectUser(Guid id)
    {
        var projectUser = await _context.ProjectUsers.FindAsync(id);

        if (projectUser == null)
        {
            return NotFound();
        }

        return projectUser;
    }
    
    // POST: api/ProjectUsers
    [HttpPost]
    public async Task<ActionResult<ProjectUserModel>> PostProjectUser(ProjectUserModel projectUserModel)
    {
        // Todo: Validierung

        _context.ProjectUsers.Add(projectUserModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProjectUser), new { id = projectUserModel.ProjectUserId }, projectUserModel);
    }
    
    // PUT: api/ProjectUsers/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> PutProjectUser(Guid id, ProjectUserModel projectUserModel)
    {
        if (id != projectUserModel.ProjectUserId)
        {
            return BadRequest();
        }

        _context.Entry(projectUserModel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProjectUserModelExists(id))
            {
                return NotFound();
            }
            throw;
        }
        return NoContent();
    }
    
    // DELETE: api/ProjectUsers/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProjectUser(Guid id)
    {
        var projectUser = await _context.ProjectUsers.FindAsync(id);

        if (projectUser == null)
        {
            return NotFound();
        }

        _context.ProjectUsers.Remove(projectUser);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    private bool ProjectUserModelExists(Guid id)
    {
        return _context.ProjectUsers.Any(e => e.ProjectUserId == id);
    }
}