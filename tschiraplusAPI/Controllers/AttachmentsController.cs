using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tschiraplusAPI.Data;
using tschiraplusAPI.Models;

namespace tschiraplusAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttachmentsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AttachmentsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // GET: api/Attachments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AttachmentModel>>> GetAttachments()
    {
        return await _context.Attachments.ToListAsync();
    }
    
    // GET: api/Attachments/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<AttachmentModel>> GetAttachment(Guid id)
    {
        var attachment = await _context.Attachments.FindAsync(id);

        if (attachment== null)
        {
            return NotFound();
        }

        return attachment;
    }
    
    // POST: api/Attachments
    [HttpPost]
    public async Task<ActionResult<AttachmentModel>> PostAttachment(AttachmentModel attachmentModel)
    {
        // Todo: Validierung

        _context.Attachments.Add(attachmentModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAttachment), new { id = attachmentModel.AttachmentId }, attachmentModel);
    }
    
    // PUT: api/Attachments/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAttachment(Guid id, AttachmentModel attachmentModel)
    {
        if (id != attachmentModel.AttachmentId)
        {
            return BadRequest();
        }

        _context.Entry(attachmentModel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AttachmentModelExists(id))
            {
                return NotFound();
            }
            throw;
        }
        return NoContent();
    }
    
    // DELETE: api/Attachments/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAttachment(Guid id)
    {
        var attachment = await _context.Attachments.FindAsync(id);

        if (attachment == null)
        {
            return NotFound();
        }

        _context.Attachments.Remove(attachment);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    private bool AttachmentModelExists(Guid id)
    {
        return _context.Attachments.Any(e => e.AttachmentId == id);
    }
}