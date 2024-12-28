using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tschiraplusAPI.Data;
using tschiraplusAPI.Models;

namespace tschiraplusAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserTaskAssignmentsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UserTaskAssignmentsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // GET: api/UserTaskAssignments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserTaskAssignmentModel>>> GetUserTaskAssignments()
    {
        return await _context.UserTaskAssignments.ToListAsync();
    }
    
    // GET: api/UserTaskAssignments/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<UserTaskAssignmentModel>> GetUserTaskAssignment(Guid id)
    {
        var userTaskAssignment = await _context.UserTaskAssignments.FindAsync(id);

        if (userTaskAssignment == null)
        {
            return NotFound();
        }

        return userTaskAssignment;
    }
    
    // POST: api/UserTaskAssignments
    [HttpPost]
    public async Task<ActionResult<UserTaskAssignmentModel>> PostUserTaskAssignment(UserTaskAssignmentModel userTaskAssignmentModel)
    {
        // Todo: Validierung

        _context.UserTaskAssignments.Add(userTaskAssignmentModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUserTaskAssignment), new { id = userTaskAssignmentModel.UserTaskAssignmentId }, userTaskAssignmentModel);
    }
    
    // PUT: api/UserTaskAssignments/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUserTaskAssignment(Guid id, UserTaskAssignmentModel userTaskAssignmentModel)
    {
        if (id != userTaskAssignmentModel.UserTaskAssignmentId)
        {
            return BadRequest();
        }

        _context.Entry(userTaskAssignmentModel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserTaskAssignmentModelExists(id))
            {
                return NotFound();
            }
            throw;
        }
        return NoContent();
    }
    
    // DELETE: api/UserTaskAssignments/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserTaskAssignment(Guid id)
    {
        var userTaskAssignment = await _context.UserTaskAssignments.FindAsync(id);

        if (userTaskAssignment == null)
        {
            return NotFound();
        }

        _context.UserTaskAssignments.Remove(userTaskAssignment);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    private bool UserTaskAssignmentModelExists(Guid id)
    {
        return _context.UserTaskAssignments.Any(e => e.UserTaskAssignmentId == id);
    }
}