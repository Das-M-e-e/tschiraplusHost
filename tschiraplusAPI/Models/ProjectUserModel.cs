using System.ComponentModel.DataAnnotations;

namespace tschiraplusAPI.Models;

public class ProjectUserModel
{
    [Key]
    public Guid ProjectUserId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public DateTime AssignedAt { get; set; }
}