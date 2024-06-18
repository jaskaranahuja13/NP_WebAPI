using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NP_WebAPI.Data.Repository.IRepository;
using NP_WebAPI.DTOs;
using NP_WebAPI.Models;

namespace NP_WebAPI.Controllers
{
    [Route("api/Trail")]
    [ApiController]
    [Authorize]
    public class TrailController : ControllerBase
    {
        ITrailRepository _trailRepository;
        private readonly IMapper _mapper;
        public TrailController(ITrailRepository trailRepository,IMapper mapper)
        {

            _mapper = mapper;
            _trailRepository = trailRepository;
        }
        [HttpGet]
        public IActionResult GetTrails()
        {
            var trailDtoList = _trailRepository.GetTrails().Select(_mapper.Map<Trail, TrailDto>);
            return Ok(trailDtoList);
        }
        [HttpGet("{trailId:int}",Name ="GetTrail")]
        public IActionResult GetTrail(int trailId)
        {
            var trail = _trailRepository.GetTrail(trailId);
            if (trail == null) return NotFound();
            var traildto = _mapper.Map<Trail, TrailDto>(trail);
            return Ok(traildto);
        }
        [HttpPost]
        public IActionResult CreateTrail([FromBody]TrailDto trailDto)
        {
            if (trailDto == null) return BadRequest();
            if (_trailRepository.TrailExists(trailDto.Name))
            {
                ModelState.AddModelError("",$"Trail in use!!!! { trailDto.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (!ModelState.IsValid) return NotFound();
            var trail = _mapper.Map<Trail>(trailDto);
            if (!_trailRepository.CreateTrail(trail))
            {
                ModelState.AddModelError("", $"Something went wrond while save data: {trailDto.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            //return Ok(trail);
            return CreatedAtRoute("GetTrail", new { trailId = trail.Id }, trail);
        }
        [HttpPut]
        public ActionResult UpdateTrail([FromBody]TrailDto trailDto)
        {
            if (trailDto == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            var trail = _mapper.Map<TrailDto, Trail>(trailDto);
            if (!_trailRepository.UpdateTrail(trail))
            {
                ModelState.AddModelError("", $"Something went wrong while update data: {trail.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteTrail(int id)
        {
            if (!_trailRepository.TrailExists(id)) return NotFound();
            var trail = _trailRepository.GetTrail(id);
            if (trail == null) return NotFound();
            if (!_trailRepository.DeleteTrail(trail))
            {
                ModelState.AddModelError("", $"Something went wrong while delete data: {trail.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }
    }
}
