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
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;

        public ScheduleController(IScheduleRepository scheduleRepository, IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ScheduleDto>))]
        public IActionResult GetSchedules()
        {
            var schedules = _scheduleRepository.GetSchedules();

            var scheduleDtos = _mapper.Map<List<ScheduleDto>>(schedules);

            return Ok(scheduleDtos);
        }

        [HttpGet("{scheduleId:guid}")]
        [ProducesResponseType(200, Type = typeof(ScheduleDto))]
        [ProducesResponseType(404)]
        public IActionResult GetSchedule(int scheduleId)
        {
            var schedule = _scheduleRepository.GetSchedule(scheduleId);

            if (schedule == null)
            {
                return NotFound();
            }

            var scheduleDto = _mapper.Map<ScheduleDto>(schedule);

            return Ok(scheduleDto);
        }


        [HttpGet("{doctorId:guid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ScheduleDto>))]
        [ProducesResponseType(404)]
        public IActionResult GetSchedulesByDoctor(int doctorId)
        {
            var schedules = _scheduleRepository.GetSchedulesByDoctor(doctorId);

            if (schedules == null || schedules.Count == 0)
            {
                return NotFound();
            }

            var scheduleDtos = _mapper.Map<List<ScheduleDto>>(schedules);

            return Ok(scheduleDtos);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ScheduleDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateSchedule([FromBody] ScheduleDto scheduleDto)
        {
            if (scheduleDto == null)
            {
                return BadRequest("Schedule data is null");
            }

            // Map the DTO to the Schedule entity
            var schedule = _mapper.Map<Schedule>(scheduleDto);

            // Add the schedule to the repository
            _scheduleRepository.CreateSchedule(schedule);

            // Save changes to the database
            if (!_scheduleRepository.Save())
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            // Return the created schedule
            return CreatedAtAction(nameof(GetSchedulesByDoctor), new { doctorId = schedule.DoctorID }, schedule);
        }

        [HttpPut("{scheduleId:guid}")]
        [ProducesResponseType(200, Type = typeof(ScheduleDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateSchedule(int scheduleId, [FromBody] ScheduleDto scheduleDto)
        {
            if (scheduleDto == null)
                return BadRequest(ModelState);

            if (scheduleId != scheduleDto.ScheduleID)
                return BadRequest(ModelState);

            if (!_scheduleRepository.ScheduleExists(scheduleId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var scheduleMap = _mapper.Map<Schedule>(scheduleDto);

            if (!_scheduleRepository.UpdateSchedule(scheduleMap))
            {
                ModelState.AddModelError("", "Something went wrong updating schedule");
                return StatusCode(500, ModelState);
            }

            _scheduleRepository.Save();

            return Ok(scheduleDto);
        }

        [HttpDelete("{scheduleId:guid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ScheduleDto))]
        public IActionResult DeleteSchedule(int scheduleId)
        {
            if (!_scheduleRepository.ScheduleExists(scheduleId))
            {
                return NotFound();
            }

            _scheduleRepository.DeleteSchedule(scheduleId);  // Pass the Guid directly
            _scheduleRepository.Save();

            var scheduleDto = _mapper.Map<ScheduleDto>(_scheduleRepository.GetSchedule(scheduleId));

            return Ok(scheduleDto);
        }


    }
}
