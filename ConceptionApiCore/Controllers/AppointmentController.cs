using AutoMapper;
using ConceptionApiCore.Dto;
using ConceptionApiCore.Interfaces;
using Doctors.Data;
using Doctors.Dto;
using Doctors.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ConceptionApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public AppointmentController(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AppointmentDto>))]
        public IActionResult GetAppointments()
        {
            var appointments = _mapper.Map<List<AppointmentDto>>(_appointmentRepository.GetAppointments());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(appointments);
        }

        [HttpGet("{appointmentId:guid}")]
        [ProducesResponseType(200, Type = typeof(AppointmentDto))]
        [ProducesResponseType(400)]
        public IActionResult GetAppointment(int appointmentId)
        {
            if (!_appointmentRepository.AppointmentExists(appointmentId))
                return NotFound();

            var appointment = _mapper.Map<AppointmentDto>(_appointmentRepository.GetAppointment(appointmentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(appointment);
        }

        [HttpGet("doctor/{doctorId:guid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AppointmentDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetAppointmentsByDoctor(int doctorId)
        {
            var appointments = _mapper.Map<List<AppointmentDto>>(_appointmentRepository.GetAppointmentsByDoctor(doctorId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(appointments);
        }

        [HttpGet("patient/{patientId:guid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AppointmentDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetAppointmentsByPatient(int patientId)
        {
            var appointments = _mapper.Map<List<AppointmentDto>>(_appointmentRepository.GetAppointmentsByPatient(patientId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(appointments);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(AppointmentDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateAppointment([FromBody] AppointmentDto appointmentDto)
        {
            if (appointmentDto == null)
                return BadRequest();

            if (_appointmentRepository.AppointmentExists(appointmentDto.AppointmentID))
            {
                ModelState.AddModelError("", "Appointment with the same ID already exists");
                return StatusCode(422, ModelState);
            }

            var appointment = _mapper.Map<Appointment>(appointmentDto);

            _appointmentRepository.CreateAppointment(appointment);

            if (!_appointmentRepository.Save())
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.AppointmentID }, _mapper.Map<AppointmentDto>(appointment));
        }

        [HttpPut("{appointmentId:guid}")]
        [ProducesResponseType(200, Type = typeof(AppointmentDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateAppointment(int appointmentId, [FromBody] AppointmentDto appointmentDto)
        {
            if (appointmentDto == null || appointmentId != appointmentDto.AppointmentID)
                return BadRequest();

            if (!_appointmentRepository.AppointmentExists(appointmentId))
                return NotFound();

            var appointment = _mapper.Map<Appointment>(appointmentDto);

            if (!_appointmentRepository.UpdateAppointment(appointment))
            {
                ModelState.AddModelError("", "Something went wrong updating appointment");
                return StatusCode(500, ModelState);
            }

            _appointmentRepository.Save();
             
            return Ok(appointmentDto);
        }


        [HttpDelete("{appointmentId:guid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(AppointmentDto))]
        public IActionResult DeleteAppointment(int appointmentId)
        {
            if (!_appointmentRepository.AppointmentExists(appointmentId))
                return NotFound();

            var appointmentToDelete = _appointmentRepository.GetAppointment(appointmentId);

            if (appointmentToDelete == null)
                return NotFound();

            _appointmentRepository.DeleteAppointment(appointmentId);
            _appointmentRepository.Save();

            return Ok(_mapper.Map<AppointmentDto>(appointmentToDelete));
        }
    }
}
