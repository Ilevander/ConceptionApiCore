using System;

namespace Doctors.Dto
{
    public class AppointmentDto
    {
        public int AppointmentID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? Description { get; set; }
        public int DoctorID { get; set; }
        public int PatientID { get; set; }
    }
}
