using ConceptionApiCore.Interfaces;
using Doctors.Data;
using Doctors.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConceptionApiCore.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly DataContext _dbContext;

        public ScheduleRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<Schedule> GetSchedules()
        {
            return _dbContext.Schedules.ToList();
        }

        public ICollection<Schedule> GetSchedulesByDoctor(Guid doctorId)
        {
            return _dbContext.Schedules
                .Where(s => s.DoctorID == doctorId)
                .ToList();
        }

        public Schedule GetSchedule(Guid scheduleId)
        {
            return _dbContext.Schedules.FirstOrDefault(s => s.ScheduleID == scheduleId);
        }

        public bool ScheduleExists(Guid scheduleId)
        {
            return _dbContext.Schedules.Any(s => s.ScheduleID == scheduleId);
        }

        public bool CreateSchedule(Schedule schedule)
        {
            if (schedule == null)
            {
                throw new ArgumentNullException(nameof(schedule));
            }

            _dbContext.Schedules.Add(schedule);
            return true; // Assuming success, you might want to add error handling.
        }

        public bool UpdateSchedule(Schedule schedule)
        {
            if (schedule == null)
            {
                throw new ArgumentNullException(nameof(schedule));
            }

            _dbContext.Entry(schedule).State = EntityState.Modified;
            return true; // Assuming success, you might want to add error handling.
        }

        public bool DeleteSchedule(Guid scheduleId)
        {
            var scheduleToDelete = _dbContext.Schedules.Find(scheduleId);
            if (scheduleToDelete == null)
            {
                return false; // Schedule not found
            }

            _dbContext.Schedules.Remove(scheduleToDelete);
            return true; // Assuming success, you might want to add error handling.
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
                // Log the error
                return false;
            }
        }
    }
}
