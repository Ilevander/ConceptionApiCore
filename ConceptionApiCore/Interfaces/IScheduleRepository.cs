using Doctors.Models;
using System;
using System.Collections.Generic;

namespace ConceptionApiCore.Interfaces
{
    public interface IScheduleRepository
    {
        ICollection<Schedule> GetSchedules();
        Schedule GetSchedule(Guid scheduleId);
        ICollection<Schedule> GetSchedulesByDoctor(Guid doctorId);
        bool ScheduleExists(Guid scheduleId);
        bool CreateSchedule(Schedule schedule);
        bool UpdateSchedule(Schedule schedule);
        bool DeleteSchedule(Guid scheduleId);
        bool Save();
    }
}
