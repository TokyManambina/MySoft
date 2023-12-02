using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftSignAPI.Dto;
using SoftSignAPI.Interfaces;
using SoftSignAPI.Model;
using SoftSignAPI.Repositories;

namespace SoftSignAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(ILogger<AuthenticationController> logger, IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        // GET: api/<userController>
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> Get([FromQuery] int? count, [FromQuery] int? page)
        {
            try
            {
                return Ok(_mapper.Map<List<UserDto>?>(await _userRepository.GetAll(count: count, page: page)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET: api/<userController>/text
        [HttpGet("find")]
        public async Task<ActionResult<List<UserDto>>> Get([FromQuery] Guid subscriptionId,[FromQuery] string search, [FromQuery] int? count, [FromQuery] int? page)
        {
            try
            {
                return Ok(_mapper.Map<List<UserDto>?>(await _userRepository.GetAll(search: search, count: count, page: page)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET api/<userController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Get(Guid id)
        {
            try
            {
                return Ok(_mapper.Map<UserDto?>(await _userRepository.Get(id)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // POST api/<userController>
        [HttpPost]
        public async Task<ActionResult<Guid>> Post([FromBody] UserDto newUser)
        {
            try
            {
                var user = await _userRepository.Create(_mapper.Map<User>(newUser));
                return Ok(user.Id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // PUT api/<userController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] UserDto updateUser)
        {
            try
            {
                return Ok(await _userRepository.Update(id, _mapper.Map<User>(updateUser)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // DELETE api/<userController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                return Ok(await _userRepository.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
