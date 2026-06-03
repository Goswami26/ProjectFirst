using System.ComponentModel.DataAnnotations;

namespace ProjectFirst.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "ProjectName is required")]
        [MaxLength(100)]
        public string ProjectName { get; set; } = string.Empty;
        public string ProjectDescription { get; set; } = string.Empty;

        // Ownership
        public int CreatorId { get; set; }
        public User ? Creator { get; set; } 

        // Tasks
        public ICollection<TaskItem> ? TaskItems { get; set; } 

        // Projectmember Reationship
        public ICollection<ProjectMember> ? ProjectMembers { get; set; } 
    }
}
