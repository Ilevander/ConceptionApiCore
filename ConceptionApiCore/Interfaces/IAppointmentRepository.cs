using Doctors.Models;

namespace ConceptionApiCore.Interfaces
{
    public interface IAppointmentRepository
    {
        ICollection<Appointment> GetAppointments();
        Appointment GetAppointment(Guid appointmentId);
        ICollection<Appointment> GetAppointmentsByDoctor(Guid doctorId);
        ICollection<Appointment> GetAppointmentsByPatient(Guid patientId);
        bool AppointmentExists(Guid appointmentId);
        bool CreateAppointment(Appointment appointment);
        bool UpdateAppointment(Appointment appointment);
        bool DeleteAppointment(Guid appointmentId);
        bool Save();
    }
}
