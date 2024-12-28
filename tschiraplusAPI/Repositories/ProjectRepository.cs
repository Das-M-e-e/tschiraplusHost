using Microsoft.EntityFrameworkCore;
using tschiraplusAPI.Data;
using tschiraplusAPI.Models;

namespace tschiraplusAPI.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly ApplicationDbContext _context;

    public ProjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<ProjectModel>> GetAllProjectsAsync()
    {
        return await _context.Projects.ToListAsync();
    }

    public async Task<ProjectModel?> GetProjectByIdAsync(Guid projectId)
    {
        return await _context.Projects.FindAsync(projectId);
    }

    public async Task<bool> ProjectExistsAsync(Guid projectId)
    {
        return await _context.Projects.AnyAsync(p => p.ProjectId == projectId);
    }

    public async Task CreateProjectAsync(ProjectModel project)
    {
        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProjectAsync(ProjectModel projectModel)
    {
        var existingProject = await GetProjectByIdAsync(projectModel.ProjectId);
        if (existingProject == null)
        {
            throw new KeyNotFoundException("Project not found.");
        }

        existingProject = projectModel;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProjectAsync(Guid projectId)
    {
        var project = await GetProjectByIdAsync(projectId);
        if (project != null)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
}