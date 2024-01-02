using AutoMapper;
using ConceptionApiCore.Dto;
using ConceptionApiCore.Interfaces;
using Doctors.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ConceptionApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        private readonly IClinicRepository _clinicRepository;
        private readonly IMapper _mapper;

        public ClinicController(IClinicRepository clinicRepository, IMapper mapper)
        {
            _clinicRepository = clinicRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClinicDto>))]
        public IActionResult GetClinics()
        {
            var clinics = _mapper.Map<List<ClinicDto>>(_clinicRepository.GetClinics());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(clinics);
        }

        [HttpGet("{clinicId:guid}")]
        [ProducesResponseType(200, Type = typeof(ClinicDto))]
        [ProducesResponseType(404)]
        public IActionResult GetClinic(Guid clinicId)
        {
            if (!_clinicRepository.ClinicExists(clinicId))
                return NotFound();

            var clinic = _mapper.Map<ClinicDto>(_clinicRepository.GetClinic(clinicId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(clinic);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ClinicDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateClinic([FromBody] ClinicDto clinicCreate)
        {
            if (clinicCreate == null)
            {
                return BadRequest("Clinic data is null");
            }

            // Check if the clinic with the same name already exists
            var existingClinic = _clinicRepository.GetClinic(clinicCreate.ClinicName);
            if (existingClinic != null)
            {
                ModelState.AddModelError("", "Clinic with the same name already exists");
                return StatusCode(422, ModelState);
            }

            // Map the DTO to the Clinic entity
            var clinic = _mapper.Map<Clinic>(clinicCreate);

            // Add the clinic to the repository
            _clinicRepository.CreateClinic(clinic);

            // Save changes to the database
            if (!_clinicRepository.Save())
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            // Return the created clinic
            return CreatedAtAction(nameof(GetClinic), new { id = clinic.ClinicID }, clinic);
        }

        [HttpPut("{clinicId:guid}")]
        [ProducesResponseType(200, Type = typeof(ClinicDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateClinic(Guid clinicId, [FromBody] ClinicDto clinicUpdate)
        {
            if (clinicUpdate == null)
                return BadRequest(ModelState);

            if (clinicId != clinicUpdate.ClinicID)
                return BadRequest(ModelState);

            if (!_clinicRepository.ClinicExists(clinicId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var clinicMap = _mapper.Map<Clinic>(clinicUpdate);

            if (!_clinicRepository.UpdateClinic(clinicMap))
            {
                ModelState.AddModelError("", "Something went wrong updating clinic");
                return StatusCode(500, ModelState);
            }

            return Ok(clinicUpdate);
        }

        [HttpDelete("{clinicId:guid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ClinicDto))]
        public IActionResult DeleteClinic(Guid clinicId)
        {
            if (!_clinicRepository.ClinicExists(clinicId))
            {
                return NotFound();
            }

            var clinicToDelete = _clinicRepository.GetClinic(clinicId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            else if (clinicToDelete == null)
                return NotFound();

            _clinicRepository.DeleteClinic(clinicId);
            _clinicRepository.Save();

            var clinicDtoToDelete = _mapper.Map<ClinicDto>(clinicToDelete);
            return Ok(clinicDtoToDelete);
        }
        [HttpGet("{clinicId:guid}/doctors")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DoctorDto>))]
        [ProducesResponseType(404)]
        public IActionResult GetDoctorsInClinic(Guid clinicId)
        {
            if (!_clinicRepository.ClinicExists(clinicId))
                return NotFound();

            var doctorsInClinic = _mapper.Map<List<DoctorDto>>(_clinicRepository.GetDoctorsInClinic(clinicId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(doctorsInClinic);
        }

        [HttpGet("{clinicId:guid}/bookings")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookingDto>))]
        [ProducesResponseType(404)]
        public IActionResult GetBookingsInClinic(Guid clinicId)
        {
            if (!_clinicRepository.ClinicExists(clinicId))
                return NotFound();

            var bookingsInClinic = _mapper.Map<List<BookingDto>>(_clinicRepository.GetBookingsInClinic(clinicId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bookingsInClinic);
        }
    }
}
