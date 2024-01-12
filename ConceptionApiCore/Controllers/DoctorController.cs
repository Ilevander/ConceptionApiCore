using AutoMapper;
using ConceptionApiCore.Dto;
using ConceptionApiCore.Interfaces;
using Doctors.Data;
using Doctors.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace ConceptionApiCore.Controllers
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base("Entity not found.")
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DoctorController> _logger;


        public DoctorController(IDoctorRepository doctorRepository, DataContext context,IMapper mapper, ILogger<DoctorController> logger)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DoctorDto>))]
        public IActionResult GetDoctors()
        {
            try
            {
                var doctors = _doctorRepository.GetDoctors();
                var doctorDtos = _mapper.Map<List<DoctorDto>>(doctors);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(doctorDtos);
            }
            catch (NotFoundException ex)
            {
                // Log the exception using ILogger
                _logger.LogWarning(ex, "Doctors not found");

                // Return a specific error message for not found scenario
                return NotFound("Doctors not found");
            }
            catch (Exception ex)
            {
                // Log the exception using ILogger
                _logger.LogError(ex, "An error occurred in GetDoctors");

                // Return a more generic error message to the client
                return StatusCode(500, "An unexpected error occurred");
            }
        }



        [HttpGet("{doctorId:guid}")]
        [ProducesResponseType(200, Type = typeof(Doctor))]
        [ProducesResponseType(400)]
        public IActionResult GetDoctor(int doctorId)
        {
            // Without using mapper : var doctors = _doctorRepository.GetDoctors();
            //Mapper do This code bellow without write it :
            /*var newDoctor = new Doctor()
            {
                DoctorName = doctors.Name;
                .
                .
                .
                .
            }*/
            if (!_doctorRepository.DoctorExists(doctorId))
                return NotFound();

            var doctor = _mapper.Map<List<DoctorDto>>(_doctorRepository.GetDoctor(doctorId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(doctor);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Doctor))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateDoctor([FromBody] DoctorDto doctorCreate)
        {
            if (doctorCreate == null)
            {
                return BadRequest("Doctor data is null");
            }

            // Check if the doctor with the same name already exists
            var existingDoctor = _doctorRepository.GetDoctor(doctorCreate.DoctorName);
            if (existingDoctor != null)
            {
                ModelState.AddModelError("", "Doctor with the same name already exists");
                return StatusCode(422, ModelState);
            }

            // Map the DTO to the Doctor entity
            var doctor = _mapper.Map<Doctor>(doctorCreate);

            // Add the doctor to the repository
            _doctorRepository.CreateDoctor(doctor);

            // Save changes to the database
            if (!_doctorRepository.Save())
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            // Return the created doctor
            return CreatedAtAction(nameof(GetDoctor), new { id = doctor.DoctorID }, doctor);
        }


        // UpdateDoctor Action
        [HttpPut("{doctorId:guid}")]
        [ProducesResponseType(200, Type = typeof(Doctor))]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDoctor(int doctorId, [FromBody] DoctorDto updateDoctor)
        {
            if (updateDoctor == null)
                return BadRequest(ModelState);

            if (doctorId != updateDoctor.DoctorID)
                return BadRequest(ModelState);

            if (!_doctorRepository.DoctorExists(doctorId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            // Map the DTO to the Doctor entity
            var doctorMap = _mapper.Map<Doctor>(updateDoctor);

            // Update the doctor using the mapped entity
            if (!_doctorRepository.UpdateDoctor(doctorMap))
            {
                ModelState.AddModelError("", "Something went wrong updating doctor");
                return StatusCode(500, ModelState);
            }

            // Save changes to the database
            _doctorRepository.Save();

            return Ok(doctorMap);
        }

        [HttpDelete("{doctorId:guid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(Doctor))]
        public IActionResult DeleteDoctor(int doctorId)
        {
            if (!_doctorRepository.DoctorExists(doctorId))
            {
                return NotFound();
            }

            var doctorToDelete = _doctorRepository.GetDoctor(doctorId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            else if (doctorToDelete == null)
                return NotFound();

            //else if (!_doctorRepository.DeleteDoctor(doctorToDelete))
            //{
            //    ModelState.AddModelError("", "Something went wrong deleting owner");
            //}
            _doctorRepository.DeleteDoctor(doctorId);
            _doctorRepository.Save();

            return Ok(doctorToDelete);
        }
    }
}
