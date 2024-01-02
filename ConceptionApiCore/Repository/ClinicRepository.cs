using System;
using System.Collections.Generic;
using System.Linq;
using ConceptionApiCore.Interfaces;
using Doctors.Data;
using Doctors.Models;
using Microsoft.EntityFrameworkCore;

namespace ConceptionApiCore.Repository
{
    public class ClinicRepository : IClinicRepository
    {
        private readonly DataContext _dbContext;

        public ClinicRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<Clinic> GetClinics()
        {
            return _dbContext.Clinics.ToList();
        }

        public Clinic GetClinic(Guid clinicId)
        {
            return _dbContext.Clinics.FirstOrDefault(c => c.ClinicID == clinicId);
        }

        public Clinic GetClinic(string clinicName)
        {
            return _dbContext.Clinics.FirstOrDefault(c => c.ClinicName == clinicName);
        }

        public bool ClinicExists(Guid clinicId)
        {
            return _dbContext.Clinics.Any(c => c.ClinicID == clinicId);
        }

        public bool CreateClinic(Clinic clinic)
        {
            _dbContext.Clinics.Add(clinic);
            return Save();
        }

        public bool UpdateClinic(Clinic clinic)
        {
            _dbContext.Entry(clinic).State = EntityState.Modified;
            return Save();
        }

        public bool DeleteClinic(Guid clinicId)
        {
            var clinicToDelete = GetClinic(clinicId);
            if (clinicToDelete == null)
                return false;

            _dbContext.Clinics.Remove(clinicToDelete);
            return Save();
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        // Implementation for additional methods with navigation properties

        public ICollection<Doctor> GetDoctorsInClinic(Guid clinicId)
        {
            return _dbContext.Doctors.Where(d => d.ClinicID == clinicId).ToList();
        }

        public ICollection<Booking> GetBookingsInClinic(Guid clinicId)
        {
            return _dbContext.Bookings.Where(b => b.ClinicID == clinicId).ToList();
        }
    }
}
