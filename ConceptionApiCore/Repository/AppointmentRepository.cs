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

        public Appointment GetAppointment(int appointmentId)
        {
            var appointment = _dbContext.Appointments.FirstOrDefault(a => a.AppointmentID == appointmentId);

            if (appointment == null)
            {
                // Handle the scenario where no appointment is found, throw an exception, or return a default value.
                throw new InvalidOperationException("Appointment not found");
            }

            return appointment;
        }



        public ICollection<Appointment> GetAppointmentsByDoctor(int doctorId)
        {
            return _dbContext.Appointments.Where(a => a.DoctorID == doctorId).ToList();
        }

        public ICollection<Appointment> GetAppointmentsByPatient(int patientId)
        {
            return _dbContext.Appointments.Where(a => a.PatientID == patientId).ToList();
        }

        public bool AppointmentExists(int appointmentId)
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

        public bool DeleteAppointment(int appointmentId)
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
