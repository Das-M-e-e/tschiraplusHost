using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tschiraplusAPI.Data;
using tschiraplusAPI.Models;

namespace tschiraplusAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public NotificationsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // GET: api/Notifications
    [HttpGet]
    public async Task<ActionResult<IEnumerable<NotificationModel>>> GetNotifications()
    {
        return await _context.Notifications.ToListAsync();
    }
    
    // GET: api/Notifications/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<NotificationModel>> GetNotification(Guid id)
    {
        var notification = await _context.Notifications.FindAsync(id);

        if (notification == null)
        {
            return NotFound();
        }

        return notification;
    }
    
    // POST: api/Notifications
    [HttpPost]
    public async Task<ActionResult<NotificationModel>> PostNotification(NotificationModel notificationModel)
    {
        // Todo: Validierung

        _context.Notifications.Add(notificationModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetNotification), new { id = notificationModel.NotificationId }, notificationModel);
    }
    
    // PUT: api/Notifications/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutNotification(Guid id, NotificationModel notificationModel)
    {
        if (id != notificationModel.NotificationId)
        {
            return BadRequest();
        }

        _context.Entry(notificationModel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!NotificationModelExists(id))
            {
                return NotFound();
            }
            throw;
        }
        return NoContent();
    }
    
    // DELETE: api/Notifications/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNotification(Guid id)
    {
        var notification = await _context.Notifications.FindAsync(id);

        if (notification == null)
        {
            return NotFound();
        }

        _context.Notifications.Remove(notification);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    private bool NotificationModelExists(Guid id)
    {
        return _context.Notifications.Any(e => e.NotificationId == id);
    }
}