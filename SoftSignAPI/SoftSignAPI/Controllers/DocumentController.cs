using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentService _documentService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IUserService _userService;

        public DocumentController(IDocumentRepository documentRepository, IMapper mapper, IWebHostEnvironment hostingEnvironment, IDocumentService documentService, IUserService userService)
        {
            _documentRepository = documentRepository;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _documentService = documentService;
            _userService = userService;
        }


        // GET: api/<DocumentController>
        [HttpGet]
        public ActionResult<List<Document>> Get([FromQuery] int? count, [FromQuery] int? page)
        {
            try
            {
                return Ok(_mapper.Map<List<Document>?>(_documentRepository.GetAll(count: count, page: page)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET: api/<DocumentController>/find?search=
        [HttpGet("find")]
        public ActionResult<List<Document>> Get([FromQuery] string search, [FromQuery] int? count, [FromQuery] int? page)
        {
            try
            {
                return Ok(_mapper.Map<List<Document>?>(_documentRepository.GetAll(search: search, count: count, page: page)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET api/<DocumentController>/5
        [HttpGet("{code}")]
        public ActionResult<Document> Get(string code)
        {
            try
            {
                return Ok(_mapper.Map<Document?>(_documentRepository.Get(code)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // POST api/<DocumentController>
        [HttpPost]
        public ActionResult<string> Post([FromBody] UploadFileDto upload)
        {
            try
            {
                if (Path.GetExtension(upload.File.FileName) != ".pdf")
                    return StatusCode(415, "Unsupported Media Type - Incorrect File Format");

                //var document = _documentService.BuildDocument(upload, _userService.GetMail()).Result;
                var document = _documentService.BuildDocument(upload, "test").Result;
                
                if(document == null)
                    return StatusCode(500, "Internal Server Error");

                return Ok(document.Code);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // DELETE api/<DocumentController>/5
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(string code)
        {
            try
            {
                return Ok(_documentRepository.Delete(code));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
