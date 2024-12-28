using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tschiraplusAPI.Data;
using tschiraplusAPI.Models;

namespace tschiraplusAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CommentController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // GET: api/Comments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentModel>>> GetComments()
    {
        return await _context.Comments.ToListAsync();
    }
    
    // GET: api/Comments/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<CommentModel>> GetComment(Guid id)
    {
        var comment = await _context.Comments.FindAsync(id);

        if (comment == null)
        {
            return NotFound();
        }

        return comment;
    }
    
    // POST: api/Comments
    [HttpPost]
    public async Task<ActionResult<CommentModel>> PostComment(CommentModel commentModel)
    {
        // Todo: Validierung

        _context.Comments.Add(commentModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetComment), new { id = commentModel.CommentId }, commentModel);
    }
    
    // PUT: api/Comments/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutComment(Guid id, CommentModel commentModel)
    {
        if (id != commentModel.CommentId)
        {
            return BadRequest();
        }

        _context.Entry(commentModel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CommentModelExists(id))
            {
                return NotFound();
            }
            throw;
        }
        return NoContent();
    }
    
    // DELETE: api/Tasks/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(Guid id)
    {
        var comment = await _context.Comments.FindAsync(id);

        if (comment == null)
        {
            return NotFound();
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    private bool CommentModelExists(Guid id)
    {
        return _context.Comments.Any(e => e.CommentId == id);
    }
}