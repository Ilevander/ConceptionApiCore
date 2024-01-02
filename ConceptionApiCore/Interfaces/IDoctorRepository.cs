using Doctors.Models;

namespace ConceptionApiCore.Interfaces
{
    public interface IDoctorRepository
    {
        ICollection<Doctor> GetDoctors();
        Doctor GetDoctor(Guid doctorId);
        Doctor GetDoctor(string doctorName);
        decimal GetDoctorPrice(Guid doctorId);
        bool DoctorExists(Guid doctorId);
        bool CreateDoctor(Doctor doctor);
        bool UpdateDoctor(Doctor doctor);
        bool DeleteDoctor(Guid doctorId);
        bool Save();
        object GetPatients();
        bool DoctorExists(Doctor doctor);
    }
}
