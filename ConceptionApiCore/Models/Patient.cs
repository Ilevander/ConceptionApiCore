using System.ComponentModel.DataAnnotations;

namespace Doctors.Models
{
    public class Patient
    {
        [Key]
        public Guid PatientID { get; set; }
        public string? Name { get; set; }
        public string? Mobile { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }

        // Navigation property for the Booking entity (One-to-Many)
        public ICollection<Booking>? Bookings { get; set; }
    }
}
