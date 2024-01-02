using System;
using System.Collections.Generic;
using Doctors.Models;

namespace ConceptionApiCore.Interfaces
{
    public interface IClinicRepository
    {
        ICollection<Clinic> GetClinics();
        Clinic GetClinic(Guid clinicId);
        ICollection<Doctor> GetDoctorsInClinic(Guid clinicId);
        ICollection<Booking> GetBookingsInClinic(Guid clinicId);
        bool ClinicExists(Guid clinicId);
        bool CreateClinic(Clinic clinic);
        bool UpdateClinic(Clinic clinic);
        bool DeleteClinic(Guid clinicId);
        bool Save();
        Clinic GetClinic(string clinicName);
    }
}
