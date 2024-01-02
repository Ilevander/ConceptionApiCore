using Doctors.Models;

namespace ConceptionApiCore.Interfaces
{
    public interface IPatientRepository
    {
        ICollection<IPatientRepository> GetPatients();
        Patient GetPatient(Guid id);
        ICollection<Booking> GetBookingByPatient(int id);
        bool PatientExists(Guid id);
        bool CreatePatient (Patient patient);
        bool UpdatePatient (Patient patient);
        bool DeletePatient (Patient patient);
        bool Save();
    }
}
