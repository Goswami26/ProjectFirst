using System.ComponentModel.DataAnnotations;

namespace ProjectFirst.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|in)$",
        ErrorMessage = "Email must end with .com or .in")]
        public string Useremail { get; set; } = string.Empty;


        public string HashedPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mobile number is required")]
        [RegularExpression(@"^[0-9]{10}$",
        ErrorMessage = "Mobile number must be exactly 10 digits")]
        public string MobileNo { get; set; } = string.Empty;

        // user can create multiple Projects
        public ICollection<Project> ? projects { get; set; }

        // user can create multiple Tasks
        public ICollection<TaskItem> ? TaskItems
        { get; set; } 

        // Projectmember Reationship
        public ICollection<ProjectMember> ? ProjectMembers
        { get; set; } 

        // TaskAssigenment Reationship
        public ICollection<TaskAssignment> ? TaskAssignments
        { get; set; }

        // Comments Reationship
        public ICollection<Comment> ? Comments
        { get; set; }

        // Userrole Reationship
        public ICollection<UserRole> ? UserRoles
        { get; set; }
    }
}
