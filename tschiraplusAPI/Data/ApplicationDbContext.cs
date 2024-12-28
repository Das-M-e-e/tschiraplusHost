using Microsoft.EntityFrameworkCore;
using tschiraplusAPI.Models;

namespace tschiraplusAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }
    
    public DbSet<AttachmentModel> Attachments { get; set; }
    public DbSet<CommentModel> Comments { get; set; }
    public DbSet<NotificationModel> Notifications { get; set; }
    public DbSet<ProjectModel> Projects { get; set; }
    public DbSet<ProjectUserModel> ProjectUsers { get; set; }
    public DbSet<SprintModel> Sprints { get; set; }
    public DbSet<TagModel> Tags { get; set; }
    public DbSet<TaskModel> Tasks { get; set; }
    public DbSet<TaskTagModel> TaskTags { get; set; }
    public DbSet<UserFriendModel> UserFriends { get; set; }
    public DbSet<UserModel> Users { get; set; }
    public DbSet<UserProjectRoleModel> UserProjectRoles { get; set; }
    public DbSet<UserSettingsModel> UserSettings { get; set; }
    public DbSet<UserTaskAssignmentModel> UserTaskAssignments { get; set; }
    
}