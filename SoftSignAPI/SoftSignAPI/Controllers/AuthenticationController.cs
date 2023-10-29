using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SoftSignAPI.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftSignAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly ILogger<AuthenticationController> _logger;
        private readonly IMapper _mapper;

        public AuthenticationController(ILogger<AuthenticationController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/<AuthenticationController>
        [HttpGet]
        [Route("")]
        public IEnumerable<string> Get()
        {
            //var a = _mapper.Map<List<User>>(await _WorkspaceRepository.GetByUserId(user.Id))
            return new string[] { "value1", "value2" };
        }
        /*
        [HttpGet]
        [Route("")]
        public IEnumerable<User> GetAll()
        {
            return null;
        }*/

        //ActionResult
        [HttpGet]
        [Route("50")]
        public ActionResult<User> Gets()
        {
            User a = new User();
            
            return Ok(new { type="success", data = a });
            return Ok();
            return BadRequest(new { type = "error", message = "User already exist." });
            return NotFound(a);
            return BadRequest(a);
        }



        // GET api/Authentication/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthenticationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AuthenticationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthenticationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
