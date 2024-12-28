using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tschiraplusAPI.Data;
using tschiraplusAPI.Models;

namespace tschiraplusAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // GET: api/Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }
    
    // GET: api/Users/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<UserModel>> GetUser(Guid id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }
    
    // POST: api/Users
    [HttpPost]
    public async Task<ActionResult<UserModel>> PostUser(UserModel userModel)
    {
        // Todo: Validierung

        _context.Users.Add(userModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = userModel.UserId }, userModel);
    }
    
    // PUT: api/Users/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(Guid id, UserModel userModel)
    {
        if (id != userModel.UserId)
        {
            return BadRequest();
        }

        _context.Entry(userModel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserModelExists(id))
            {
                return NotFound();
            }
            throw;
        }
        return NoContent();
    }
    
    // DELETE: api/Users/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    private bool UserModelExists(Guid id)
    {
        return _context.Users.Any(e => e.UserId == id);
    }
}