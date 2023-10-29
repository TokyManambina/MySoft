using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SoftSignAPI.Model;
using SoftSignAPI.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftSignAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocietyController : ControllerBase
    {

        private readonly IRepository<Society> _societyRepository;
        private readonly IMapper _mapper;

        public SocietyController(ILogger<AuthenticationController> logger, IMapper mapper, IRepository<Society> societyRepository)
        {
            _mapper = mapper;
            _societyRepository= societyRepository;
        }

        // GET: api/<OfferController>
        [HttpGet]
        public ActionResult<List<Society>> Get([FromQuery] int? count, [FromQuery] int? page)
        {
            try
            {
                return Ok(_mapper.Map<List<Society>?>(_societyRepository.GetAll(count: count, page: page)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET api/<OfferController>/5
        [HttpGet("{id}")]
        public ActionResult<Society> Get(int id)
        {
            try
            {
                if (_societyRepository.IsExist(id))
                    return Ok(_mapper.Map<Society?>(_societyRepository.Get(id)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // POST api/<OfferController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<OfferController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OfferController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
