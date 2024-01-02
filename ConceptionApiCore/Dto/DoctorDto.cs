namespace ConceptionApiCore.Dto
{
    //DTOs are often used to transfer only the necessary data between layers of an application, and they help to avoid exposing the entire entity to external systems or clients.
    public class DoctorDto
    {
        public Guid DoctorID { get; set; }
        public string? DoctorName { get; set; }
        public string? Specialization { get; set; }

        // Foreign key for the Clinic entity (Many-to-One)
        public int ClinicID { get; set; }
    }
}
