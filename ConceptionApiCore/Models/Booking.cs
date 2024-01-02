using System.ComponentModel.DataAnnotations;

namespace Doctors.Models
{
    public class Booking
    {
        [Key]
        public Guid BookingID { get; set; }
        public DateTime BookingDate { get; set; }

        // Foreign key for the Patient entity
        public Guid PatientID { get; set; }
        public Patient? Patient { get; set; }

        // Foreign key for the Clinic entity
        public Guid ClinicID { get; set; }
        public Clinic? Clinic { get; set; }
    }
}
