
using Doctors.Models;
using System;
using System.Collections.Generic;

namespace ConceptionApiCore.Interfaces
{
    public interface IBookingRepository
    {
        ICollection<Booking> GetBookings();
        Booking GetBooking(Guid bookingId);
        ICollection<Booking> GetBookingsByPatient(Guid patientId);
        bool BookingExists(Guid bookingId);
        bool CreateBooking(Booking booking);
        bool UpdateBooking(Booking booking);
        bool DeleteBooking(Guid bookingId);
        bool Save();
    }
}
