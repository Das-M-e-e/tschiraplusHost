using System.ComponentModel.DataAnnotations;

namespace tschiraplusAPI.Models;

public class CommentModel
{
    [Key]
    public Guid CommentId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid? TaskId { get; set; }
    public Guid AuthorId { get; set; }
    public Guid? ParentCommentId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastUpdatedTime { get; set; }
    public bool isEdited { get; set; }
    public bool isDeleted { get; set; }
}