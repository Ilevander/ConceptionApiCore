
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

        public Booking GetBooking(int bookingId)
        {
            var booking = _dbContext.Bookings.FirstOrDefault(b => b.BookingID == bookingId);

            if (booking == null)
            {
                // Handle the scenario where no booking is found, throw an exception, or return a default value.
                throw new InvalidOperationException("Booking not found");
            }

            return booking;
        }

        public ICollection<Booking> GetBookingsByPatient(int patientId)
        {
            return _dbContext.Bookings.Where(b => b.PatientID == patientId).ToList();
        }

        public bool BookingExists(int bookingId)
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

        public bool DeleteBooking(int bookingId)
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
