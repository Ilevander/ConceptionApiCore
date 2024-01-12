using Doctors.Models;

namespace ConceptionApiCore.Interfaces
{
    public interface IDoctorRepository
    {
        ICollection<Doctor> GetDoctors();
        Doctor? GetDoctor(int doctorId);
        Doctor? GetDoctor(string? doctorName);
        decimal GetDoctorPrice(int doctorId);
        bool DoctorExists(int doctorId);
        bool CreateDoctor(Doctor doctor);
        bool UpdateDoctor(Doctor doctor);
        bool DeleteDoctor(int doctorId);
        bool Save();
        object GetPatients();
        bool DoctorExists(Doctor doctor);
    }
}
