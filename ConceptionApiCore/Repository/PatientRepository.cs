using ConceptionApiCore.Interfaces;
using Doctors.Data;
using Doctors.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConceptionApiCore.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly DataContext _dbContext;

        public PatientRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<Patient> GetPatients()
        {
            return _dbContext.Patients.ToList();
        }

        public Patient? GetPatient(int id)
        {
            return _dbContext.Patients
                .Include( p => p.Bookings)
                .FirstOrDefault(p => p.PatientID == id);
        }


        public ICollection<Booking> GetBookingsByPatient(int id)
        {
            var patient = _dbContext.Patients.Include(p => p.Bookings).FirstOrDefault(p => p.PatientID == id);
            return patient?.Bookings.ToList();
        }


        public bool PatientExists(int id)
        {
            return _dbContext.Patients.Any(p => p.PatientID == id);
        }

        public bool CreatePatient(Patient patient)
        {
            _dbContext.Patients.Add(patient);
            return Save();
        }

        public bool UpdatePatient(Patient patient)
        {
            _dbContext.Patients.Update(patient);
            return Save();
        }

        public bool DeletePatient(Patient patient)
        {
            _dbContext.Patients.Remove(patient);
            return Save();
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() > 0;
        }
        //Or :
        /*public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }*/
        ICollection<IPatientRepository> IPatientRepository.GetPatients()
        {
            throw new NotImplementedException();
        }

        public ICollection<Booking> GetBookingByPatient(int id)
        {
            throw new NotImplementedException();
        }
    }
}
