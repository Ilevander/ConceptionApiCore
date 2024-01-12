namespace ConceptionApiCore.Dto
{
    //DTOs are often used to transfer only the necessary data between layers of an application, and they help to avoid exposing the entire entity to external systems or clients.
    public class PatientDto
    {
        public int PatientID { get; set; }
        public string? Name { get; set; }
        public string? Mobile { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
    }
}
