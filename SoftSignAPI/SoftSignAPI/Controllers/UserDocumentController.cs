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
    [Route("api")]
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
    //    [HttpGet("documents/u")]
    //    public async Task<ActionResult> Get([FromQuery] Guid? userId, [FromQuery] string? code, [FromQuery] int? count, [FromQuery] int? page)
    //    {
    //        try
    //        {
    //            var all = await _userDocumentRepository.GetAll(userId, code, count, page);
    //            var ds = _mapper.Map<List<DocumentByUserDto>>(all);
				//return Ok(ds);
    //        }
    //        catch (Exception ex)
    //        {
    //            return StatusCode(500, "Internal Server Error");
    //        }
    //    }

		[HttpGet("document/u/{code}")]
		public async Task<ActionResult> Get([FromQuery] Guid? userId, [FromQuery] string? code)
		{
			try
			{
				var udoc = await _userDocumentRepository.Get(userId: userId, code: code);

                if (udoc == null)
                    return NotFound("Document not found");


				return Ok(new {
                    Step = udoc.Step,
                    Document = udoc.Document,
					hasSignature = udoc.Fields.Any(x => x.FieldType == FieldType.Signature),
				    hasParaphe = udoc.Fields.Any(x => x.FieldType == FieldType.Paraphe)
			    });
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal Server Error");
			}
		}


		//// GET api/<UserDocumentController>/5
		//[HttpGet("document/d/{id}")]
  //      public async Task<ActionResult<UserDocument>> Get(int id)
  //      {
  //          try
  //          {
  //              return Ok(await _userDocumentRepository.Get(id));
  //          }
  //          catch (Exception ex)
  //          {
  //              return StatusCode(500, "Internal Server Error");
  //          }
  //      }

  //      // POST api/<UserDocumentController>
  //      [HttpPost("document/u/{code}")]
  //      public ActionResult Post([FromQuery]string code, [FromBody] List<UserRoleDocumentDto> users)
  //      {
  //          try
  //          {
  //              var insert = _userDocumentService.LinkUserWithDocument(code, users);
  //              if (insert.IsCanceled || insert.IsFaulted)
  //                  return StatusCode(500, "Internal Server Error");
  //              return Ok();
  //          }
  //          catch (Exception ex)
  //          {
  //              return StatusCode(500, "Internal Server Error");
  //          }
  //      }

  //      // PUT api/<UserDocumentController>/5
  //      [HttpPut("document/u/{code}")]

		//public async Task<ActionResult> Put(string code, [FromQuery] Guid userId)
  //      {
  //          try
  //          {
  //              return Ok(await _userDocumentRepository.Update(code, userId));
  //          }
  //          catch (Exception ex)
  //          {
  //              return StatusCode(500, "Internal Server Error");
  //          }
  //      }

  //      // PUT api/<UserDocumentController>/5
  //      [HttpPut("document/d/{id}")]
  //      public async Task<ActionResult> Put(int id, [FromBody] UserDocumentDto userDocument)
  //      {
  //          try
  //          {
  //              return Ok(await _userDocumentRepository.Update(id, _mapper.Map<UserDocument>(userDocument)));
  //          }
  //          catch (Exception ex)
  //          {
  //              return StatusCode(500, "Internal Server Error");
  //          }
  //      }

  //      // DELETE api/<UserDocumentController>/5
  //      [HttpDelete("{id}")]
  //      public async Task<ActionResult> Delete(int id)
  //      {
  //          try
  //          {
  //              return Ok(await _userDocumentRepository.Delete(id));
  //          }
  //          catch (Exception ex)
  //          {
  //              return StatusCode(500, "Internal Server Error");
  //          }
  //      }
    }
}
