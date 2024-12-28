using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tschiraplusAPI.Data;
using tschiraplusAPI.Models;

namespace tschiraplusAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProjectController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // GET: api/Projects
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectModel>>> GetProjects()
    {
        return await _context.Projects.ToListAsync();
    }
    
    // GET: api/Projects/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectModel>> GetProject(Guid id)
    {
        var project = await _context.Projects.FindAsync(id);

        if (project == null)
        {
            return NotFound();
        }

        return project;
    }
    
    // POST: api/Projects
    [HttpPost]
    public async Task<ActionResult<ProjectModel>> PostProject(ProjectModel projectModel)
    {
        // Todo: Validierung

        _context.Projects.Add(projectModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProject), new { id = projectModel.ProjectId }, projectModel);
    }
    
    // PUT: api/Projects/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProject(Guid id, ProjectModel projectModel)
    {
        if (id != projectModel.ProjectId)
        {
            return BadRequest();
        }

        _context.Entry(projectModel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProjectModelExists(id))
            {
                return NotFound();
            }
            throw;
        }
        return NoContent();
    }
    
    // DELETE: api/Projects/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(Guid id)
    {
        var project = await _context.Projects.FindAsync(id);

        if (project == null)
        {
            return NotFound();
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    private bool ProjectModelExists(Guid id)
    {
        return _context.Projects.Any(e => e.ProjectId == id);
    }
}