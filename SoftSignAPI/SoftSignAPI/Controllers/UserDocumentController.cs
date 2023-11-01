using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SoftSignAPI.Dto;
using SoftSignAPI.Interfaces;
using SoftSignAPI.Model;
using SoftSignAPI.Repositories;
using SoftSignAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftSignAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDocumentController : ControllerBase
    {
        private readonly IUserDocumentRepository _userDocumentRepository;
        private readonly IMapper _mapper;
        private readonly IUserDocumentService _userDocumentService;

        public UserDocumentController(IUserDocumentRepository userDocumentRepository, IMapper mapper, IUserDocumentService userDocumentService)
        {
            _userDocumentRepository = userDocumentRepository;
            _mapper = mapper;
            _userDocumentService = userDocumentService;
        }


        // GET: api/<UserDocumentController>
        [HttpGet]
        public async Task<ActionResult<List<DocumentByUserDto>>> Get([FromQuery] Guid? userId, [FromQuery] string? code, [FromQuery] int? count, [FromQuery] int? page)
        {
            try
            {
                var a = await _userDocumentRepository.GetAll(userId, code, count, page);
                var ds = _mapper.Map<List<DocumentByUserDto>>(a);
				return Ok(ds);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }


		// GET api/<UserDocumentController>/5
		[HttpGet("{id}")]
        public async Task<ActionResult<UserDocument>> Get(int id)
        {
            try
            {
                return Ok(await _userDocumentRepository.Get(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // POST api/<UserDocumentController>
        [HttpPost]
        public ActionResult Post([FromQuery]string code, [FromBody] List<UserRoleDocumentDto> users)
        {
            try
            {
                var insert = _userDocumentService.LinkUserWithDocument(code, users);
                if (insert.IsCanceled || insert.IsFaulted)
                    return StatusCode(500, "Internal Server Error");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // PUT api/<UserDocumentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserDocumentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
