namespace ProjectFirst.Models
{
    public class TaskItem
    {
        public int TaskItemId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Duedate { get; set; }
        public string Status { get; set; } = string.Empty;

        // Ownership
        public int CreatorId { get; set; }
        public User ? Creator { get; set; } 

        // Project Relation
        public int ProjectId { get; set; }
        public Project ? Project { get; set; } 

        // TaskAssigenment Reationship
        public ICollection<TaskAssignment> ? TaskAssignments { get; set; }

        // comment Reationship
        public ICollection<Comment>? Comments { get; set; }
    }
}
