namespace ConceptionApiCore.Dto
{
    public class ScheduleDto
    {
        public int ScheduleID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int DoctorID { get; set; }
    }
}
