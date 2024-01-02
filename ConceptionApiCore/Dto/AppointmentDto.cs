using System;

namespace Doctors.Dto
{
    public class AppointmentDto
    {
        public Guid AppointmentID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? Description { get; set; }
        public Guid DoctorID { get; set; }
        public Guid PatientID { get; set; }
    }
}
