
using System;

namespace ConceptionApiCore.Dto
{
    public class BookingDto
    {
        public int BookingID { get; set; }
        public DateTime BookingDate { get; set; }
        public int PatientID { get; set; }
        public int ClinicID { get; set; }
    }
}
