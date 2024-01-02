using System.ComponentModel.DataAnnotations;

namespace Doctors.Models
{
    public class Clinic
    {
        [Key]
        public Guid ClinicID { get; set; }
        public string? ClinicName { get; set; }
        public string? Location { get; set; }

        // Navigation property for the Doctor entity (One-to-Many)
        public ICollection<Doctor>? Doctors { get; set; }

        // Navigation property for the Booking entity (One-to-Many)
        public ICollection<Booking>? Bookings { get; set; }
    }
}
