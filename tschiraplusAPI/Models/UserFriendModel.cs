using System.ComponentModel.DataAnnotations;

namespace tschiraplusAPI.Models;

public class UserFriendModel
{
    [Key]
    public Guid UserFriendId { get; set; }
    public Guid UserId { get; set; }
    public Guid FriendId { get; set; }
    public DateTime BefriendedAt { get; set; }
}