
using ConceptionApiCore.Interfaces;
using Doctors.Data;
using Doctors.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConceptionApiCore.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DataContext _dbContext;

        public BookingRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<Booking> GetBookings()
        {
            return _dbContext.Bookings.ToList();
        }

        public Booking GetBooking(Guid bookingId)
        {
            return _dbContext.Bookings.FirstOrDefault(b => b.BookingID == bookingId);
        }
        public ICollection<Booking> GetBookingsByPatient(Guid patientId)
        {
            return _dbContext.Bookings.Where(b => b.PatientID == patientId).ToList();
        }

        public bool BookingExists(Guid bookingId)
        {
            return _dbContext.Bookings.Any(b => b.BookingID == bookingId);
        }

        public bool CreateBooking(Booking booking)
        {
            _dbContext.Bookings.Add(booking);
            return true;
        }
        public bool UpdateBooking(Booking booking)
        {
            _dbContext.Entry(booking).State = EntityState.Modified;
            return true;
        }

        public bool DeleteBooking(Guid bookingId)
        {
            var bookingToDelete = _dbContext.Bookings.Find(bookingId);
            if (bookingToDelete == null)
                return false;

            _dbContext.Bookings.Remove(bookingToDelete);
            return true;
        }

        public bool Save()
        {
            try
            {
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
