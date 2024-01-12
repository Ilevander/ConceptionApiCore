using Doctors.Models;
using System;
using System.Collections.Generic;

namespace ConceptionApiCore.Interfaces
{
    public interface IScheduleRepository
    {
        ICollection<Schedule> GetSchedules();
        Schedule GetSchedule(int scheduleId);
        ICollection<Schedule> GetSchedulesByDoctor(int doctorId);
        bool ScheduleExists(int scheduleId);
        bool CreateSchedule(Schedule schedule);
        bool UpdateSchedule(Schedule schedule);
        bool DeleteSchedule(int scheduleId);
        bool Save();
    }
}
