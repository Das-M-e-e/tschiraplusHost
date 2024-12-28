using System.ComponentModel.DataAnnotations;

namespace tschiraplusAPI.Models;

public class NotificationModel
{
    [Key]
    public Guid NotificationId { get; set; }
    public Guid AuthorId { get; set; }
    public Guid RecipientId { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public int Type { get; set; }
    public int Severity { get; set; }
    public DateTime Timestamp { get; set; }
    public bool isRead { get; set; }
}