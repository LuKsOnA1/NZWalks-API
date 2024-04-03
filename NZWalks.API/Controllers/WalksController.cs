using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs.Walk;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
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
        public async Task<IActionResult> GetAll()
        {
            var domainModel = await _repository.GetAllAsync();

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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateWalk([FromBody] CreateWalkRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Something went wrong. Try again later!");
            }

            var domainModel = _mapper.Map<Walk>(model);

            await _repository.CreateAsync(domainModel);

            var result = _mapper.Map<WalkDTO>(domainModel);

            return Ok(result);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, UpdateWalkRequestDTO model)
        {
            var domainModel = _mapper.Map<Walk>(model);

            if (!ModelState.IsValid)
            {
                return BadRequest("Something went wrong. Try again later!");
            }

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
