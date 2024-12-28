using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tschiraplusAPI.Data;
using tschiraplusAPI.Models;

namespace tschiraplusAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserSettingsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UserSettingsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // GET: api/UserSettings
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserSettingsModel>>> GetUserSettingss()
    {
        return await _context.UserSettings.ToListAsync();
    }
    
    // GET: api/UserSettings/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<UserSettingsModel>> GetUserSettings(Guid id)
    {
        var userSettings = await _context.UserSettings.FindAsync(id);

        if (userSettings == null)
        {
            return NotFound();
        }

        return userSettings;
    }
    
    // POST: api/UserSettings
    [HttpPost]
    public async Task<ActionResult<UserSettingsModel>> PostUserSettings(UserSettingsModel userSettingsModel)
    {
        // Todo: Validierung

        _context.UserSettings.Add(userSettingsModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUserSettings), new { id = userSettingsModel.UserSettingsId }, userSettingsModel);
    }
    
    // PUT: api/UserSettings/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUserSettings(Guid id, UserSettingsModel userSettingsModel)
    {
        if (id != userSettingsModel.UserSettingsId)
        {
            return BadRequest();
        }

        _context.Entry(userSettingsModel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserSettingsModelExists(id))
            {
                return NotFound();
            }
            throw;
        }
        return NoContent();
    }
    
    // DELETE: api/UserSettings/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserSettings(Guid id)
    {
        var userSettings = await _context.UserSettings.FindAsync(id);

        if (userSettings == null)
        {
            return NotFound();
        }

        _context.UserSettings.Remove(userSettings);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    private bool UserSettingsModelExists(Guid id)
    {
        return _context.UserSettings.Any(e => e.UserSettingsId == id);
    }
}