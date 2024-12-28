using System.ComponentModel.DataAnnotations;

namespace tschiraplusAPI.Models;

public class UserModel
{
    [Key]
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? Bio { get; set; }
    public int Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastLogin { get; set; }
}