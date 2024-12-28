using tschiraplusAPI.Models;

namespace tschiraplusAPI.Repositories;

public interface IProjectRepository
{
    Task<IEnumerable<ProjectModel>> GetAllProjectsAsync();
    Task<ProjectModel?> GetProjectByIdAsync(Guid projectId);
    Task<bool> ProjectExistsAsync(Guid projectId);
    Task CreateProjectAsync(ProjectModel project);
    Task UpdateProjectAsync(ProjectModel projectModel);
    Task DeleteProjectAsync(Guid projectId);
}