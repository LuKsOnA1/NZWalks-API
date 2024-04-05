using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs.Region;
using NZWalks.API.Repositories.API.Abstract;

namespace NZWalks.API.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _repository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Reader, Writer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await _repository.GetAllAsync();

            var result = _mapper.Map<List<RegionDTO>>(regions);

            return Ok(result);
        }


        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader, Writer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            var region = await _repository.GetAsync(id);
            if (region == null)
            {
                return NotFound("Region with given id does not exist!");
            }

            var result = _mapper.Map<RegionDTO>(region);

            return Ok(result);
        }


        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateRegion([FromBody] CreateRegionRequestDto model)
        {

            var domainModel = _mapper.Map<Region>(model);

            domainModel = await _repository.CreateAsync(domainModel);

            var regionDTO = _mapper.Map<RegionDTO>(domainModel);

            return CreatedAtAction(nameof(GetRegionById), new { id = regionDTO.Id }, regionDTO);
        }


        [HttpPut]
        [ValidateModel]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto model)
        {

            var regionDomainModel = _mapper.Map<Region>(model);

            regionDomainModel = await _repository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }


            var result = _mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(result);
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RemoveRegion([FromRoute] Guid id)
        {
            var regionDomainModel = await _repository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound("Region with given id does not exist!");
            }

            return NoContent();
        }

    }
}
