﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs.Walk;
using NZWalks.API.Repositories.API.Abstract;

namespace NZWalks.API.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IWalkRepository _repository;

        public WalksController(IMapper mapper, IWalkRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
        {
            var domainModel = await _repository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            var result = _mapper.Map<List<WalkDTO>>(domainModel);

            return Ok(result);
        }


        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var domainModel = await _repository.GetAsync(id);
            if (domainModel == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<WalkDTO>(domainModel);
            return Ok(result);
        }


        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateWalk([FromBody] CreateWalkRequestDto model)
        {

            var domainModel = _mapper.Map<Walk>(model);

            await _repository.CreateAsync(domainModel);

            var result = _mapper.Map<WalkDTO>(domainModel);

            return Ok(result);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, UpdateWalkRequestDTO model)
        {

            var domainModel = _mapper.Map<Walk>(model);

            domainModel = await _repository.UpdateAsync(id, domainModel);
            if (domainModel == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<WalkDTO>(domainModel);
            return Ok(result);
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemoveWalk([FromRoute] Guid id)
        {
            var walk = await _repository.DeleteAsync(id);
            if (walk == null)
            {
                return NotFound("Record with given id does not exist!");
            }

            return NoContent();
        }

    }
}
