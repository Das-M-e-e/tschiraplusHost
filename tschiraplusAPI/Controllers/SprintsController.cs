using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tschiraplusAPI.Data;
using tschiraplusAPI.Models;

namespace tschiraplusAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SprintsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SprintsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // GET: api/Sprints
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SprintModel>>> GetSprints()
    {
        return await _context.Sprints.ToListAsync();
    }
    
    // GET: api/Sprints/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<SprintModel>> GetSprint(Guid id)
    {
        var sprint = await _context.Sprints.FindAsync(id);

        if (sprint == null)
        {
            return NotFound();
        }

        return sprint;
    }
    
    // POST: api/Sprints
    [HttpPost]
    public async Task<ActionResult<SprintModel>> PostSprint(SprintModel sprintModel)
    {
        // Todo: Validierung

        _context.Sprints.Add(sprintModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSprint), new { id = sprintModel.SprintId }, sprintModel);
    }
    
    // PUT: api/Sprints/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSprint(Guid id, SprintModel sprintModel)
    {
        if (id != sprintModel.SprintId)
        {
            return BadRequest();
        }

        _context.Entry(sprintModel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SprintModelExists(id))
            {
                return NotFound();
            }
            throw;
        }
        return NoContent();
    }
    
    // DELETE: api/Sprints/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSprint(Guid id)
    {
        var sprint = await _context.Sprints.FindAsync(id);

        if (sprint == null)
        {
            return NotFound();
        }

        _context.Sprints.Remove(sprint);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    private bool SprintModelExists(Guid id)
    {
        return _context.Sprints.Any(e => e.SprintId == id);
    }
}