using System.ComponentModel.DataAnnotations;

namespace tschiraplusAPI.Models;

public class TaskModel
{
    [Key]
    public Guid TaskId { get; init; }
    public Guid ProjectId { get; set; }
    public Guid? SprintId { get; set; }
    public Guid AuthorId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? Status { get; set; }
    public int? Priority { get; set; }
    public DateTime CreationDate { get; init; }
    public DateTime? DueDate { get; set; }
    public DateTime? CompletionDate { get; set; }
    public DateTime LastUpdated { get; set; }
    public long? EstimatedTime { get; set; }
    public long? ActualTimeSpent { get; set; }
}