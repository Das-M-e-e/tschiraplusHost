using System.ComponentModel.DataAnnotations;

namespace tschiraplusAPI.Models;

public class UserTaskAssignmentModel
{
    [Key]
    public Guid UserTaskAssignmentId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public Guid TaskId { get; set; }
    public Guid AssignedBy { get; set; }
    public DateTime AssignedDate { get; set; }
}