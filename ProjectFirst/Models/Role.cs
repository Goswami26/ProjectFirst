namespace ProjectFirst.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;

        // Userrole Reationship
        public ICollection<UserRole>? UserRoles
        { get; set; }
    }
}
