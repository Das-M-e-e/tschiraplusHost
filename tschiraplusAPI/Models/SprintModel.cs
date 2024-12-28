using System.ComponentModel.DataAnnotations;

namespace tschiraplusAPI.Models;

public class SprintModel
{
    [Key]
    public Guid SprintId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid AuthorId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? LastUpdated { get; set; }
}