using System.ComponentModel.DataAnnotations;

namespace Doctors.Models
{
    public class User
    {
        [Key]
        public Guid UserID { get; set; }
        public Guid RoleID { get; set; }
        public string? Username { get; set; } = string .Empty;
        public string? PasswordHash { get; set; } = string.Empty;
        public string? Email { get; set; }
        public DateTime Date { get; set; }
        public string? Address { get; set; }

        // Navigation property for the Role entity (Many-to-One)
        public Role? Role { get; set; }
    }

}
