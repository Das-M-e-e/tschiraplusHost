using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tschiraplusAPI.Data;
using tschiraplusAPI.Models;

namespace tschiraplusAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserFriendsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UserFriendsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // GET: api/UserFriends
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserFriendModel>>> GetUserFriends()
    {
        return await _context.UserFriends.ToListAsync();
    }
    
    // GET: api/UserFriends/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<UserFriendModel>> GetUserFriend(Guid id)
    {
        var userFriend = await _context.UserFriends.FindAsync(id);

        if (userFriend == null)
        {
            return NotFound();
        }

        return userFriend;
    }
    
    // POST: api/UserFriends
    [HttpPost]
    public async Task<ActionResult<UserFriendModel>> PostUserFriend(UserFriendModel userFriendModel)
    {
        // Todo: Validierung

        _context.UserFriends.Add(userFriendModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUserFriend), new { id = userFriendModel.UserFriendId }, userFriendModel);
    }
    
    // PUT: api/UserFriends/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUserFriend(Guid id, UserFriendModel userFriendModel)
    {
        if (id != userFriendModel.UserFriendId)
        {
            return BadRequest();
        }

        _context.Entry(userFriendModel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserFriendModelExists(id))
            {
                return NotFound();
            }
            throw;
        }
        return NoContent();
    }
    
    // DELETE: api/UserFriends/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserFriend(Guid id)
    {
        var userFriend = await _context.UserFriends.FindAsync(id);

        if (userFriend == null)
        {
            return NotFound();
        }

        _context.UserFriends.Remove(userFriend);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    private bool UserFriendModelExists(Guid id)
    {
        return _context.UserFriends.Any(e => e.UserFriendId == id);
    }
}