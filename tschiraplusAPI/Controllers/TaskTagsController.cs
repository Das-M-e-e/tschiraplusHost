using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tschiraplusAPI.Data;
using tschiraplusAPI.Models;

namespace tschiraplusAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskTagsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TaskTagsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // GET: api/TaskTags
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskTagModel>>> GetTaskTags()
    {
        return await _context.TaskTags.ToListAsync();
    }
    
    // GET: api/TaskTags/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskTagModel>> GetTaskTag(Guid id)
    {
        var taskTag = await _context.TaskTags.FindAsync(id);

        if (taskTag == null)
        {
            return NotFound();
        }

        return taskTag;
    }
    
    // POST: api/TaskTags
    [HttpPost]
    public async Task<ActionResult<TaskTagModel>> PostTaskTag(TaskTagModel taskTagModel)
    {
        // Todo: Validierung

        _context.TaskTags.Add(taskTagModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTaskTag), new { id = taskTagModel.TaskTagId }, taskTagModel);
    }
    
    // PUT: api/TaskTags/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTaskTag(Guid id, TaskTagModel taskTagModel)
    {
        if (id != taskTagModel.TaskTagId)
        {
            return BadRequest();
        }

        _context.Entry(taskTagModel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TaskTagModelExists(id))
            {
                return NotFound();
            }
            throw;
        }
        return NoContent();
    }
    
    // DELETE: api/TaskTags/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTaskTag(Guid id)
    {
        var taskTag = await _context.TaskTags.FindAsync(id);

        if (taskTag == null)
        {
            return NotFound();
        }

        _context.TaskTags.Remove(taskTag);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    private bool TaskTagModelExists(Guid id)
    {
        return _context.TaskTags.Any(e => e.TaskTagId == id);
    }
}