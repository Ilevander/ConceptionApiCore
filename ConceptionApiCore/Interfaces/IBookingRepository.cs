
using Doctors.Models;
using System;
using System.Collections.Generic;

namespace ConceptionApiCore.Interfaces
{
    public interface IBookingRepository
    {
        ICollection<Booking> GetBookings();
        Booking GetBooking(int bookingId);
        ICollection<Booking> GetBookingsByPatient(int patientId);
        bool BookingExists(int bookingId);
        bool CreateBooking(Booking booking);
        bool UpdateBooking(Booking booking);
        bool DeleteBooking(int bookingId);
        bool Save();
    }
}
