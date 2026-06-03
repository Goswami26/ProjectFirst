using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectFirst.Models;
namespace ProjectFirst.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<TaskAssignment>  Assignments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ProjectMember> Members { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> userRoles { get; set; }

        // Fluent API

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);

                entity.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(u => u.Useremail)
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(u => u.MobileNo)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

                // Unique constraints (very important in real apps)
                entity.HasIndex(u => u.Useremail).IsUnique();
                entity.HasIndex(u => u.MobileNo).IsUnique();

            });

            // Project
            modelBuilder.Entity<Project>( entity =>
            {
                entity.HasKey(p => p.ProjectId);

                entity.Property(p => p.ProjectName)
                .IsRequired()
                .HasMaxLength(150);

                entity.Property(p => p.ProjectDescription)
                .IsRequired()
                .HasMaxLength(200);

                // creator relationship (user -> projects)
                entity.HasOne(u => u.Creator)
                .WithMany(p => p.projects)
                .HasForeignKey(u => u.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            // TaskItem
            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.HasKey(t => t.TaskItemId);

                entity.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(150);

                entity.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(200);

                entity.Property(t => t.Status)
                .HasMaxLength(20);

                // Project relationship (project -> taskitems)
                entity.HasOne(t => t.Project)
                .WithMany(p => p.TaskItems)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

                // creator relationship (user -> taskitems)
                entity.HasOne(t => t.Creator)
                .WithMany(p => p.TaskItems)
                .HasForeignKey(t => t.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

                // TaskAssigenment Reationship

            });

            // Comment
            modelBuilder.Entity<Comment>( entity =>
            {
                entity.HasKey(c => c.CommentId);

                entity.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(500);

                // User Relationship (user -> comment)
                entity.HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                // Task Relationship (comment -> task)
                entity.HasOne(c => c.TaskItem)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.Taskid)
                .OnDelete(DeleteBehavior.Cascade);

            });


            // ProjectMember
            modelBuilder.Entity<ProjectMember>(entity =>
            {
                entity.HasKey(pm => new{ pm.UserId, pm.ProjectId});

                entity.Property(pm => pm.Role)
                .IsRequired()
                .HasMaxLength(100);

                // User relation 
                entity.HasOne(pm => pm.User)
                .WithMany(u => u.ProjectMembers)
                .HasForeignKey(pm => pm.UserId)
                .OnDelete(DeleteBehavior.Cascade);

                // Project relation
                entity.HasOne(pm => pm.Project)
                .WithMany(p => p.ProjectMembers)
                .HasForeignKey(pm => pm.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            // TaskAssignment
            modelBuilder.Entity<TaskAssignment>( entity =>
            {
                entity.HasKey(ta => new {ta.UserId, ta.TaskItemId});

                entity.HasOne(ta => ta.User)
                .WithMany(u => u.TaskAssignments)
                .HasForeignKey(ta => ta.UserId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ta => ta.TaskItem)
                .WithMany(u => u.TaskAssignments)
                .HasForeignKey(ta => ta.TaskItemId)
                .OnDelete(DeleteBehavior.Cascade);

            });

            // Role
            modelBuilder.Entity<Role>( entity =>
            {
                entity.HasKey(r => r.RoleId);

                entity.Property(r => r.RoleName)
                .IsRequired()
                .HasMaxLength(100);

            });

            // UserRole
            modelBuilder.Entity<UserRole>( entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });

                entity.HasOne(ur => ur.user)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ur => ur.Role)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
} 
