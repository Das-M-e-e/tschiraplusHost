using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tschiraplusAPI.Data;
using tschiraplusAPI.Models;

namespace tschiraplusAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectUserController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProjectUserController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // GET: api/ProjectUsers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectUserModel>>> GetProjectUsers()
    {
        return await _context.ProjectUsers.ToListAsync();
    }
    
    // GET: api/ProjectUsers/{id}
    [HttpGet("{id}")]
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
    [HttpPut("{id}")]
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
    [HttpDelete("{id}")]
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