using AutoMapper;
using ConceptionApiCore.Dto;
using Doctors.Dto;
using Doctors.Models;


namespace ConceptionApiCore.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Doctor , DoctorDto>();
            CreateMap<Patient, PatientDto>();
            CreateMap<Fee, FeeDto>();
            CreateMap<Clinic , ClinicDto>();
            CreateMap<Appointment, AppointmentDto>();
            CreateMap<Schedule, ScheduleDto>();
            CreateMap<Booking, BookingDto>();
        }    
        
    }
}
