using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tschiraplusAPI.Data;
using tschiraplusAPI.Models;

namespace tschiraplusAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TagsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // GET: api/Tags
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TagModel>>> GetTags()
    {
        return await _context.Tags.ToListAsync();
    }
    
    // GET: api/Tags/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<TagModel>> GetTag(Guid id)
    {
        var tag = await _context.Tags.FindAsync(id);

        if (tag == null)
        {
            return NotFound();
        }

        return tag;
    }
    
    // POST: api/Tags
    [HttpPost]
    public async Task<ActionResult<TagModel>> PostTag(TagModel tagModel)
    {
        // Todo: Validierung

        _context.Tags.Add(tagModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTag), new { id = tagModel.TagId }, tagModel);
    }
    
    // PUT: api/Tags/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTag(Guid id, TagModel tagModel)
    {
        if (id != tagModel.TagId)
        {
            return BadRequest();
        }

        _context.Entry(tagModel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TagModelExists(id))
            {
                return NotFound();
            }
            throw;
        }
        return NoContent();
    }
    
    // DELETE: api/Tags/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTag(Guid id)
    {
        var tag = await _context.Tags.FindAsync(id);

        if (tag == null)
        {
            return NotFound();
        }

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    private bool TagModelExists(Guid id)
    {
        return _context.Tags.Any(e => e.TagId == id);
    }
}