namespace ConceptionApiCore.Dto
{
    public class ScheduleDto
    {
        public Guid ScheduleID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid DoctorID { get; set; }
    }
}
