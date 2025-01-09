using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tschiraplusAPI.Data;
using tschiraplusAPI.Models;

namespace tschiraplusAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TasksController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // GET: api/Tasks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasks()
    {
        return await _context.Tasks.ToListAsync();
    }
    
    // GET: api/Tasks/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskModel>> GetTask(Guid id)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task == null)
        {
            return NotFound();
        }

        return task;
    }
    
    // POST: api/Tasks
    [HttpPost]
    public async Task<ActionResult<TaskModel>> PostTask(TaskModel taskModel)
    {
        // Todo: Validierung

        await _context.Tasks.AddAsync(taskModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTask), new { id = taskModel.TaskId }, taskModel);
    }
    
    // PUT: api/Tasks/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTask(Guid id, TaskModel taskModel)
    {
        if (id != taskModel.TaskId)
        {
            return BadRequest();
        }

        _context.Entry(taskModel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TaskModelExists(id))
            {
                return NotFound();
            }
            throw;
        }
        return NoContent();
    }
    
    // DELETE: api/Tasks/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task == null)
        {
            return NotFound();
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    private bool TaskModelExists(Guid id)
    {
        return _context.Tasks.Any(e => e.TaskId == id);
    }
}