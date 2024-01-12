using System.ComponentModel.DataAnnotations;

namespace Doctors.Models
{
    public class Fee
    {
        [Key]
        public int FeeID { get; set; }
        public decimal Amount { get; set; }
        //Foreign key for doctor (One-To-Many)
        public int DoctorID { get; set; }

        // Navigation property for the Doctor entity (Many-to-One)
        public Doctor? Doctor { get; set; }
    }
}
