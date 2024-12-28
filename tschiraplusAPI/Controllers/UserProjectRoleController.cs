using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tschiraplusAPI.Data;
using tschiraplusAPI.Models;

namespace tschiraplusAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserProjectRoleController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UserProjectRoleController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // GET: api/UserProjectRoles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserProjectRoleModel>>> GetUserProjectRoles()
    {
        return await _context.UserProjectRoles.ToListAsync();
    }
    
    // GET: api/UserProjectRoles/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<UserProjectRoleModel>> GetUserProjectRole(Guid id)
    {
        var userProjectRole = await _context.UserProjectRoles.FindAsync(id);

        if (userProjectRole == null)
        {
            return NotFound();
        }

        return userProjectRole;
    }
    
    // POST: api/UserProjectRoles
    [HttpPost]
    public async Task<ActionResult<UserProjectRoleModel>> PostUserProjectRole(UserProjectRoleModel userProjectRoleModel)
    {
        // Todo: Validierung

        _context.UserProjectRoles.Add(userProjectRoleModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUserProjectRole), new { id = userProjectRoleModel.UserProjectRoleId }, userProjectRoleModel);
    }
    
    // PUT: api/UserProjectRoles/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUserProjectRole(Guid id, UserProjectRoleModel userProjectRoleModel)
    {
        if (id != userProjectRoleModel.UserProjectRoleId)
        {
            return BadRequest();
        }

        _context.Entry(userProjectRoleModel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserProjectRoleModelExists(id))
            {
                return NotFound();
            }
            throw;
        }
        return NoContent();
    }
    
    // DELETE: api/UserProjectRoles/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserProjectRole(Guid id)
    {
        var userProjectRole = await _context.UserProjectRoles.FindAsync(id);

        if (userProjectRole == null)
        {
            return NotFound();
        }

        _context.UserProjectRoles.Remove(userProjectRole);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    private bool UserProjectRoleModelExists(Guid id)
    {
        return _context.UserProjectRoles.Any(e => e.UserProjectRoleId == id);
    }
}