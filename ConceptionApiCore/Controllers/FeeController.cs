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
    public class FeeController : ControllerBase
    {
        private readonly IFeeRepository _feeRepository;
        private readonly IMapper _mapper;

        public FeeController(IFeeRepository feeRepository, DataContext context, IMapper mapper)
        {
            _feeRepository = feeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FeeDto>))]
        public IActionResult GetFees()
        {
            var fees = _mapper.Map<List<FeeDto>>(_feeRepository.GetFees());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(fees);
        }

        [HttpGet("{feeId:guid}")]
        [ProducesResponseType(200, Type = typeof(FeeDto))]
        [ProducesResponseType(400)]
        public IActionResult GetFee(Guid feeId)
        {
            if (!_feeRepository.FeeExists(feeId))
                return NotFound();

            var fee = _mapper.Map<FeeDto>(_feeRepository.GetFee(feeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(fee);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(FeeDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateFee([FromBody] FeeDto feeDto)
        {
            if (feeDto == null)
            {
                return BadRequest("Fee data is null");
            }

            // Check if the fee with the same ID already exists
            if (_feeRepository.FeeExists(feeDto.FeeID))
            {
                ModelState.AddModelError("", "Fee with the same ID already exists");
                return StatusCode(422, ModelState);
            }

            // Map the DTO to the Fee entity
            var fee = _mapper.Map<Fee>(feeDto);

            // Add the fee to the repository
            _feeRepository.CreateFee(fee);

            // Save changes to the database
            if (!_feeRepository.Save())
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            // Return the created fee
            return CreatedAtAction(nameof(GetFee), new { id = fee.FeeID }, fee);
        }


        [HttpPut("{feeId:guid}")]
        [ProducesResponseType(200, Type = typeof(FeeDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public IActionResult UpdateFee(Guid feeId, [FromBody] FeeDto feeDto)
        {
            if (feeDto == null || feeId != feeDto.FeeID)
                return BadRequest(ModelState);

            if (!_feeRepository.FeeExists(feeId))
                return NotFound();

            var fee = _mapper.Map<Fee>(feeDto);

            if (!_feeRepository.UpdateFee(fee))
            {
                ModelState.AddModelError("", "Something went wrong updating fee");
                return StatusCode(500, ModelState);
            }

            _feeRepository.Save();

            return Ok(_mapper.Map<FeeDto>(fee));
        }

        [HttpDelete("{feeId:guid}")]
        [ProducesResponseType(200, Type = typeof(Fee))]
        [ProducesResponseType(404)]
        public IActionResult DeleteFee(Guid feeId)
        {
            var feeToDelete = _feeRepository.GetFee(feeId);

            if (feeToDelete == null)
            {
                return NotFound();
            }

            if (!_feeRepository.DeleteFee(feeId))
            {
                ModelState.AddModelError("", "Something went wrong deleting the fee");
                return StatusCode(500, ModelState);
            }

            _feeRepository.Save();

            return Ok(feeToDelete);
        }

    }
}
