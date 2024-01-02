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
    public class PatientController : Controller
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientController(IPatientRepository patientRepository, DataContext context,IMapper mapper)
        {
            _patientRepository = patientRepository;//That gives access to the database
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Patient>))]
        public IActionResult GetPatients()
        {
            var patiens = _mapper.Map<List<PatientDto>>(_patientRepository.GetPatients());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(patiens);
        }


        [HttpGet("{patientId}")]
        [ProducesResponseType(200, Type = typeof(Patient))]
        [ProducesResponseType(400)]
        public IActionResult GetPatient(Guid patientId)
        {

            if (!_patientRepository.PatientExists(patientId))
                return NotFound();

            var patient = _mapper.Map<PatientDto>(_patientRepository.GetPatient(patientId));
           
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (patient == null)
                return NotFound();

            return Ok(patient);
        }



        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Patient))]
        [ProducesResponseType(400)]
        public IActionResult CreatePatient([FromBody] PatientDto patientCreate)
        {
            if (patientCreate == null)
            {
                return BadRequest("Patient data is null");
            }

            // Map the DTO to the Patient entity
            var patient = _mapper.Map<Patient>(patientCreate);

            // Add the patient to the repository
            _patientRepository.CreatePatient(patient);

            // Save changes to the database
            if (!_patientRepository.Save())
            {
                return StatusCode(500, "Something went wrong while saving");
            }

            // Return the created patient
            return CreatedAtAction(nameof(GetPatient), new { id = patient.PatientID }, patient);
        }


        [HttpPut("{patientId}")]
        [ProducesResponseType(200, Type = typeof(Patient))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdatePatient(Guid patientId, [FromBody] PatientDto updatePatient)
        {
            if (updatePatient == null || patientId != updatePatient.PatientID)
            {
                return BadRequest("Invalid patient data");
            }

            if (!_patientRepository.PatientExists(patientId))
            {
                return NotFound();
            }

            // Map the DTO to the Patient entity
            var patient = _mapper.Map<Patient>(updatePatient);

            if (!_patientRepository.UpdatePatient(patient))
            {
                return StatusCode(500, "Something went wrong updating patient");
            }

            _patientRepository.Save();

            return Ok(patient);
        }


        [HttpDelete("{patientId}")]
        [ProducesResponseType(200, Type = typeof(Patient))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeletePatient(Guid patientId)
        {
            var patientToDelete = _patientRepository.GetPatient(patientId);

            if (patientToDelete == null)
            {
                return NotFound();
            }

            // Map the entity to the DTO
            var patientDto = _mapper.Map<PatientDto>(patientToDelete);

            // Delete the patient
            _patientRepository.DeletePatient(patientToDelete);
            _patientRepository.Save();

            // Return the deleted patient DTO
            return Ok(patientDto);
        }

        /*[HttpDelete("{patientId:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePatient(Guid patientId)
        {
            var patientToDelete = _patientRepository.GetPatient(patientId);
            if (patientToDelete == null)
            {
                return NotFound();
            }

            var bookingsToDelete = _patientRepository.GetBookingByPatient(patientId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_patientRepository.DeletePatient(patientToDelete))
            {
                ModelState.AddModelError("", "Something went wrong when deleting patient");
                return BadRequest(ModelState);
            }

            _patientRepository.Save();

            return NoContent();
        }*/

    }
}
