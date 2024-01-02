using AutoMapper;
using ConceptionApiCore.Dto;
using ConceptionApiCore.Interfaces;
using Doctors.Data;
using Doctors.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ConceptionApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingController(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Booking>))]
        public IActionResult GetBookings()
        {
            var bookings = _mapper.Map<List<BookingDto>>(_bookingRepository.GetBookings());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bookings);
        }

        [HttpGet("{bookingId:guid}")]
        [ProducesResponseType(200, Type = typeof(Booking))]
        [ProducesResponseType(400)]
        public IActionResult GetBooking(Guid bookingId)
        {
            if (!_bookingRepository.BookingExists(bookingId))
                return NotFound();

            var booking = _mapper.Map<BookingDto>(_bookingRepository.GetBooking(bookingId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(booking);
        }

        [HttpGet("patient/{patientId:int}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Booking>))]
        [ProducesResponseType(400)]
        public IActionResult GetBookingsByPatient(Guid patientId)
        {
            var bookings = _mapper.Map<List<BookingDto>>(_bookingRepository.GetBookingsByPatient(patientId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bookings);
        }



        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Booking))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateBooking([FromBody] BookingDto bookingCreate)
        {
            if (bookingCreate == null)
                return BadRequest("Booking data is null");

            // Map the DTO to the Booking entity
            var booking = _mapper.Map<Booking>(bookingCreate);

            // Add the booking to the repository
            _bookingRepository.CreateBooking(booking);

            // Save changes to the database
            if (!_bookingRepository.Save())
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            // Return the created booking
            return CreatedAtAction(nameof(GetBooking), new { id = booking.BookingID }, booking);
        }

        [HttpPut("{bookingId:guid}")]
        [ProducesResponseType(200, Type = typeof(Booking))]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBooking(Guid bookingId, [FromBody] BookingDto updateBooking)
        {
            if (updateBooking == null)
                return BadRequest(ModelState);

            if (bookingId != updateBooking.BookingID)
                return BadRequest(ModelState);

            if (!_bookingRepository.BookingExists(bookingId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var bookingMap = _mapper.Map<Booking>(updateBooking);

            if (!_bookingRepository.UpdateBooking(bookingMap))
            {
                ModelState.AddModelError("", "Something went wrong updating booking");
                return StatusCode(500, ModelState);
            }

            _bookingRepository.UpdateBooking(bookingMap);
            _bookingRepository.Save();

            return Ok(bookingMap);
        }

        [HttpDelete("{bookingId:guid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(Booking))]
        public IActionResult DeleteBooking(Guid bookingId)
        {
            if (!_bookingRepository.BookingExists(bookingId))
            {
                return NotFound();
            }

            var bookingToDelete = _bookingRepository.GetBooking(bookingId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            else if (bookingToDelete == null)
                return NotFound();

            _bookingRepository.DeleteBooking(bookingId);
            _bookingRepository.Save();

            return Ok(bookingToDelete);
        }
    }
}
