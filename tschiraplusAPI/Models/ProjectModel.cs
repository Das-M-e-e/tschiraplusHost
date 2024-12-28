using System.ComponentModel.DataAnnotations;

namespace tschiraplusAPI.Models;

public class ProjectModel
{
    [Key]
    public Guid ProjectId { get; set; }
    public Guid OwnerId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int Status { get; set; }
    public int Priority { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? LastUpdated { get; set; }
}