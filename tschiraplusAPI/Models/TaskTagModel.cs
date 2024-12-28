using System.ComponentModel.DataAnnotations;

namespace tschiraplusAPI.Models;

public class TaskTagModel
{
    [Key]
    public Guid TaskTagId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid TaskId { get; set; }
    public Guid TagId { get; set; }
    public Guid AssignedBy { get; set; }
    public DateTime AssignedAt { get; set; }
}