using Microsoft.AspNetCore.Mvc;
using tschiraplusAPI.Models;
using tschiraplusAPI.Repositories;

namespace tschiraplusAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IProjectRepository _projectRepository;

    public ProjectsController(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    
    // GET: api/Projects
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectModel>>> GetProjects()
    {
        var projects = await _projectRepository.GetAllProjectsAsync();
        return Ok(projects);
    }
    
    // GET: api/Projects/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProjectModel>> GetProject(Guid id)
    {
        var project = await _projectRepository.GetProjectByIdAsync(id);

        if (project == null)
        {
            return NotFound();
        }

        return Ok(project);
    }
    
    // POST: api/Projects
    [HttpPost]
    public async Task<ActionResult<ProjectModel>> PostProject(ProjectModel projectModel)
    {
        var projectExists = await _projectRepository.ProjectExistsAsync(projectModel.ProjectId);
        if (projectExists)
        {
            return BadRequest("Project already exists");
        }

        await _projectRepository.CreateProjectAsync(projectModel);
        return CreatedAtAction(nameof(GetProject), new { id = projectModel.ProjectId }, projectModel);
    }
    
    // PUT: api/Projects/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> PutProject(Guid id, ProjectModel projectModel)
    {
        if (id != projectModel.ProjectId)
        {
            return BadRequest("Project ID mismatch.");
        }
        
        try
        {
            await _projectRepository.UpdateProjectAsync(projectModel);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Project not found.");
        }
        return NoContent();
    }
    
    // DELETE: api/Projects/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProject(Guid id)
    {
        await _projectRepository.DeleteProjectAsync(id);
        return NoContent();
    }
}