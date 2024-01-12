using Doctors.Models;

namespace ConceptionApiCore.Interfaces
{
    public interface IAppointmentRepository
    {
        ICollection<Appointment> GetAppointments();
        Appointment? GetAppointment(int appointmentId);
        ICollection<Appointment> GetAppointmentsByDoctor(int doctorId);
        ICollection<Appointment> GetAppointmentsByPatient(int patientId);
        bool AppointmentExists(int appointmentId);
        bool CreateAppointment(Appointment appointment);
        bool UpdateAppointment(Appointment appointment);
        bool DeleteAppointment(int appointmentId);
        bool Save();
    }
}
