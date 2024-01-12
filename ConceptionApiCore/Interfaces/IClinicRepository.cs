using System;
using System.Collections.Generic;
using Doctors.Models;

namespace ConceptionApiCore.Interfaces
{
    public interface IClinicRepository
    {
        ICollection<Clinic> GetClinics();
        Clinic GetClinic(int clinicId);
        ICollection<Doctor> GetDoctorsInClinic(int clinicId);
        ICollection<Booking> GetBookingsInClinic(int clinicId);
        bool ClinicExists(int clinicId);
        bool CreateClinic(Clinic clinic);
        bool UpdateClinic(Clinic clinic);
        bool DeleteClinic(int clinicId);
        bool Save();
        Clinic GetClinic(string clinicName);
    }
}
