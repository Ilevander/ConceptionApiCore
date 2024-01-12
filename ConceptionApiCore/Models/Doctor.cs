using System.ComponentModel.DataAnnotations;

namespace Doctors.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorID { get; set; }
        public string? DoctorName { get; set; }
        public string? Specialization { get; set; }

        // Foreign key for the Clinic entity (Many-to-One)
        public int ClinicID { get; set; }

        // Navigation property for the Clinic entity (Many-to-One)
        public Clinic? Clinic { get; set; }

        // Navigation property for the Fees entity (One-to-Many)
        public ICollection<Fee>? Fees { get; set; }

        // Navigation property for the Schedule entity (One-to-Many)
        public ICollection<Schedule>? Schedules { get; set; }
    }

}
