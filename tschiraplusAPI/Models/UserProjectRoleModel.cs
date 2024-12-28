using System.ComponentModel.DataAnnotations;

namespace tschiraplusAPI.Models;

public class UserProjectRoleModel
{
    [Key]
    public Guid UserProjectRoleId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public Guid AssignedBy { get; set; }
    public int Role { get; set; }
    public DateTime AssignedDate { get; set; }
    public bool IsActive { get; set; }
}