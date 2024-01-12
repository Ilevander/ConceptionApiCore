using ConceptionApiCore.Interfaces;
using Doctors.Data;
using Doctors.Models;
using Microsoft.EntityFrameworkCore;

namespace ConceptionApiCore.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DataContext _dbContext;

        public DoctorRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<Doctor> GetDoctors()
        {
            return _dbContext.Doctors.OrderBy(p=>p.DoctorID).ToList();
        }

        public Doctor? GetDoctor(int doctorId)
        {
            return _dbContext.Doctors.Where(p => p.DoctorID == doctorId).FirstOrDefault();
        }

        public Doctor? GetDoctor(string? doctorName)
        {
            return _dbContext.Doctors.Where(d => d.DoctorName == doctorName).FirstOrDefault();
        }

        public decimal GetDoctorPrice(int doctorId)
        {
            var fees = _dbContext.Doctors.Include(d => d.Fees).FirstOrDefault(d => d.DoctorID == doctorId)?.Fees;
            return fees?.Sum(f => f.Amount) ?? 0;
        }


        public bool DoctorExists(int doctorId)
        {
            return _dbContext.Doctors.Any(d => d.DoctorID == doctorId);
        }

        public bool CreateDoctor(Doctor doctor)
        {
            _dbContext.Doctors.Add(doctor);
            return Save();
        }

        public bool UpdateDoctor(Doctor doctor)
        {
            _dbContext.Doctors.Update(doctor);
            return Save();
        }

        public bool DeleteDoctor(int doctorId)
        {
            var doctor = _dbContext.Doctors.Find(doctorId);
            if (doctor != null)
            {
                _dbContext.Doctors.Remove(doctor);
                return Save();
            }
            return false;
        }

        public bool Save()
        {
            try
            {
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public object GetPatients()
        {
            throw new NotImplementedException();
        }

        public bool DoctorExists(Doctor doctor)
        {
            throw new NotImplementedException();
        }
        //Or 
        /*public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }*/
    }
}
