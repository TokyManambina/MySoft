using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoftSignAPI.Dto;
using SoftSignAPI.Interfaces;
using SoftSignAPI.Model;
using SoftSignAPI.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftSignAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly ISocietyRepository _societyRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public DocumentController(IDocumentRepository documentRepository, IMapper mapper, IWebHostEnvironment hostingEnvironment, ISocietyRepository societyRepository)
        {
            _documentRepository = documentRepository;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _societyRepository = societyRepository;
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
        public ActionResult Post([FromForm] FileUploadRequest upload, [FromBody] DocumentDto newDocument)
        {
            try
            {
                /*var uploadFile = upload.FileName.Replace(" ", "_");
                string filename = Path.GetFileNameWithoutExtension(uploadFile);
                string extension = Path.GetExtension(uploadFile);
                if (extension != ".pdf")
                    return StatusCode(415, "Unsupported Media Type - Incorrect File Format");
                */
                var date = DateTime.Now;
                newDocument.DateSend = date;
                newDocument.Code = $"{Convert.ToHexString(BitConverter.GetBytes(date.Ticks))}-{date.ToString("yyyyMM")}";


                //filename = newDocument.DateSend.Value.ToString("yyyyMMdd-") + filename;
                
                return RedirectToRoute("/sdf");

                var urlDoc = $"{_hostingEnvironment.WebRootPath}/Documents/{""}";

                var document = _mapper.Map<Document>(newDocument);
                
                return Ok(_documentRepository.Create(document));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // PUT api/<DocumentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DocumentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
    public class FileUploadRequest
    {
        public string? UploaderName { get; set; }
        public string? UploaderAddress { get; set; }
        public IFormFile? File { get; set; }
    }
}
