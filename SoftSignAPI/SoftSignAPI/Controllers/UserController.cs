using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftSignAPI.Dto;
using SoftSignAPI.Interfaces;
using SoftSignAPI.Model;
using SoftSignAPI.Repositories;
using SoftSignAPI.Services;
using System.Collections;

namespace SoftSignAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _UserService;



        public UserController(IMapper mapper, IUserService userService, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _UserService = userService;
        }

        // GET: api/<userController>
        [HttpGet("get")]
        public async Task<ActionResult<List<UserDto>>> Get([FromQuery] int? count, [FromQuery] int? page)
        {
            try
            {
                var user=await _userRepository.GetByMail(_UserService.GetMail());
                bool status = user?.Role==Role.Admin? true:false;
                var lista = await _userRepository.GetAll(count: count, page: page);
                if(user?.Role==Role.Admin)
                {
                    return Ok(new { role=status,data= _mapper.Map<List<UserDto>?>(lista) });
                }
                else
                {
                    return Ok(new { role = status, data = _mapper.Map<List<UserDto>?>(lista!.Where(u => u.SocietyId == user?.SocietyId)) });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("GetListRole")]
        public ActionResult<List<object>> GetListRole()
        {
            try
            {
                var role = Enum.GetValues(typeof(Role)).OfType<Role>().ToList().Select(u => new { value = u, name=Enum.GetName(typeof(Role),u)}) ;
                return Ok(role);
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
        [HttpGet("statistique")]
        public async Task<ActionResult> Getstatistique([FromQuery] Guid subscriptionId, [FromQuery] int? count, [FromQuery] int? page)
        {
            try
            {
                var user = await _userRepository.GetByMail(_UserService.GetMail());
                var list = await _userRepository.GetAll(count: count, page: page);
                var max_user = user?.Subscription?.Capacity;
                var curr_user = user?.SocietyId==null? list?.Count(): list?.Where(u=>u.SocietyId==user?.SocietyId).Count();
                var reste_user=max_user-curr_user;

                return Ok(new {max_user=max_user,curr_user=curr_user,reste_user=reste_user});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET api/<userController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> FindById(Guid id)
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
