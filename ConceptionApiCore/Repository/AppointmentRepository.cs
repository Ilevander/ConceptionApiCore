using System;
using System.Collections.Generic;
using System.Linq;
using ConceptionApiCore.Interfaces;
using Doctors.Data;
using Doctors.Models;

namespace Doctors.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DataContext _dbContext;

        public AppointmentRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<Appointment> GetAppointments()
        {
            return _dbContext.Appointments.ToList();
        }

        public Appointment GetAppointment(Guid appointmentId)
        {
            return _dbContext.Appointments.FirstOrDefault(a => a.AppointmentID == appointmentId);
        }

        public ICollection<Appointment> GetAppointmentsByDoctor(Guid doctorId)
        {
            return _dbContext.Appointments.Where(a => a.DoctorID == doctorId).ToList();
        }

        public ICollection<Appointment> GetAppointmentsByPatient(Guid patientId)
        {
            return _dbContext.Appointments.Where(a => a.PatientID == patientId).ToList();
        }

        public bool AppointmentExists(Guid appointmentId)
        {
            return _dbContext.Appointments.Any(a => a.AppointmentID == appointmentId);
        }

        public bool CreateAppointment(Appointment appointment)
        {
            _dbContext.Appointments.Add(appointment);
            return true;
        }

        public bool UpdateAppointment(Appointment appointment)
        {
            _dbContext.Appointments.Update(appointment);
            return true;
        }

        public bool DeleteAppointment(Guid appointmentId)
        {
            var appointmentToDelete = _dbContext.Appointments.Find(appointmentId);
            if (appointmentToDelete != null)
            {
                _dbContext.Appointments.Remove(appointmentToDelete);
                return true;
            }
            return false;
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() > 0;
        }
    }
}
