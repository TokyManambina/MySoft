using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftSignAPI.Dto;
using SoftSignAPI.Interfaces;
using SoftSignAPI.Model;

namespace SoftSignAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldController : ControllerBase
    {
        private readonly IFieldRepository _fieldRepository;
        private readonly IMapper _mapper;

        public FieldController(ILogger<AuthenticationController> logger, IMapper mapper, IFieldRepository fieldRepository)
        {
            _mapper = mapper;
            _fieldRepository = fieldRepository;
        }

        // GET: api/<fieldController>
        [HttpGet]
        public async Task<ActionResult<List<FieldDto>>> Get([FromQuery] int? count, [FromQuery] int? page)
        {
            try
            {
                return Ok(_mapper.Map<List<FieldDto>?>(await _fieldRepository.GetAll(count: count, page: page)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET api/<fieldController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FieldDto>> Get(int id)
        {
            try
            {
                return Ok(_mapper.Map<FieldDto?>(await _fieldRepository.Get(id)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // POST api/<fieldController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] FieldDto newField)
        {
            try
            {
                var field = await _fieldRepository.Create(_mapper.Map<Field>(newField));
                return Ok(field.Id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // PUT api/<fieldController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] FieldDto updateField)
        {
            try
            {
                return Ok(await _fieldRepository.Update(id, _mapper.Map<Field>(updateField)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // DELETE api/<fieldController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                return Ok(await _fieldRepository.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
