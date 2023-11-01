using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SoftSignAPI.Model;
using SoftSignAPI.Interfaces;
using SoftSignAPI.Repositories;
using SoftSignAPI.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftSignAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocietyController : ControllerBase
    {

        private readonly ISocietyRepository _societyRepository;
        private readonly IMapper _mapper;

        public SocietyController(ILogger<AuthenticationController> logger, IMapper mapper, ISocietyRepository societyRepository)
        {
            _mapper = mapper;
            _societyRepository= societyRepository;
        }

        // GET: api/<SocietyController>
        [HttpGet]
        public async Task<ActionResult<List<SocietyDto>>> Get([FromQuery] int? count, [FromQuery] int? page)
        {
            try
            {
                return Ok(_mapper.Map<List<SocietyDto>?>(await _societyRepository.GetAll(count: count, page: page)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET: api/<SocietyController>/text
        [HttpGet("find")]
        public async Task<ActionResult<List<SocietyDto>>> Get([FromQuery] string search,[FromQuery] int? count, [FromQuery] int? page)
        {
            try
            {
                return Ok(_mapper.Map<List<SocietyDto>?>(await _societyRepository.GetAll(search, count: count, page: page)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET api/<SocietyController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SocietyDto>> Get(Guid id)
        {
            try
            {
                return Ok(_mapper.Map<SocietyDto?>(await _societyRepository.Get(id)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // POST api/<SocietyController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SocietyDto newSociety)
        {
            try
            {
                return Ok(await _societyRepository.Create(_mapper.Map<Society>(newSociety)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // PUT api/<SocietyController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] Society updateSociety)
        {
            try
            {
                return Ok(await _societyRepository.Update(id, updateSociety));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // DELETE api/<SocietyController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                return Ok(_societyRepository.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
