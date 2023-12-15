using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PdfSharp.Drawing;
using SoftSignAPI.Dto;
using SoftSignAPI.Interfaces;
using SoftSignAPI.Model;
using SoftSignAPI.Repositories;
using SoftSignAPI.Services;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftSignAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentService _documentService;
        private readonly IUserDocumentService _userDocumentService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IUserDocumentRepository _userDocumentRepository;
        private readonly IPdfService _pdfService;

        public DocumentController(
            IDocumentRepository documentRepository, 
            IMapper mapper, 
            IWebHostEnvironment hostingEnvironment, 
            IDocumentService documentService, 
            IUserService userService, 
            IUserRepository userRepository,
            IUserDocumentService userDocumentService,
            IUserDocumentRepository userDocumentRepository,
            IPdfService pdfService)
        {
            _documentRepository = documentRepository;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _documentService = documentService;
            _userService = userService;
            _userRepository = userRepository;
            _userDocumentService = userDocumentService;
            _userDocumentRepository = userDocumentRepository;
            _pdfService = pdfService;
        }


		// GET: api/<DocumentController>
		[HttpGet]
        public async Task<ActionResult<List<DocumentDto>>> Get([FromQuery] int? count, [FromQuery] int? page)
        {
            try
            {
                return Ok(_mapper.Map<List<DocumentDto>>(await _documentRepository.GetAll(count: count, page: page)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET: api/<DocumentController>/find?search=
        [HttpGet("find")]
        public async Task<ActionResult<List<Document>>> Get([FromQuery] string search, [FromQuery] int? count, [FromQuery] int? page)
        {
            try
            {
                return Ok(_mapper.Map<List<Document>?>(await _documentRepository.GetAll(search: search, count: count, page: page)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
		// GET: api/<DocumentController>/filter/posted?userId=
		[HttpGet("filter/{stat}")]
		[Authorize]
		public async Task<ActionResult<List<ShowDocument>>> GetSignedDocuments(DocumentStat stat, [FromQuery] string? search, [FromQuery] int? count, [FromQuery] int? page)
		{
			try
			{
				var user = await _userRepository.GetByMail(_userService.GetMail());
				if (user == null)
					return SignOut("Logout");

				var documents = await _documentRepository.GetDocuments(user: user, stat, search: search, count: count, page: page);

				return Ok(documents);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal Server Error");
			}
		}
		[HttpGet("filter/owned")]
		[Authorize]
		public async Task<ActionResult<List<ShowDocument>>> GetOwnerDocument([FromQuery] string? search, [FromQuery] int? count, [FromQuery] int? page)
		{
			try
			{
				var user = await _userRepository.GetByMail(_userService.GetMail());
				if (user == null)
					return SignOut("Logout");

                var documents = await _documentRepository.GetOwnerDocument(user: user, search: search, count: count, page: page);

                


				return Ok(documents);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal Server Error");
			}
		}
		[HttpGet("filter/posted")]
        [Authorize]
        public async Task<ActionResult<List<ShowDocument>>> GetSendedDocument([FromQuery] string? search, [FromQuery] int? count, [FromQuery] int? page)
        {
            try
            {
                var user = await _userRepository.GetByMail(_userService.GetMail());
                if(user == null)
					return SignOut("Logout");
                var documents = await _documentRepository.GetSendedDocument(user: user, search: search, count: count, page: page);

				return Ok(documents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        // GET: api/<DocumentController>/filter/received?userId=
        [HttpGet("filter/received")]
		[Authorize]
		public async Task<ActionResult<List<ShowDocument>>> GetReceivedDocument([FromQuery] string? search, [FromQuery] int? count, [FromQuery] int? page)
        {
            try
            {
				var user = await _userRepository.GetByMail(_userService.GetMail());
				if (user == null)
					return SignOut("Logout");

				var documents = await _documentRepository.GetReceivedDocument(user: user, search: search, count: count, page: page);

				return Ok(documents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("u/info")]
        [Authorize]
        public async Task<ActionResult<List<DocInfo>>> GetInfo()
        {
            try
            {
				var user = await _userRepository.GetByMail(_userService.GetMail());
				if (user == null)
					return SignOut("Logout");
				return Ok(await _documentRepository.GetDocumentInfo(user.Id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        // GET api/<DocumentController>/5
        [HttpGet("{code}")]
        [Authorize]
        public async Task<ActionResult<DocumentDto>> Get(string code)
        {
            try
            {
				var user = await _userRepository.GetByMail(_userService.GetMail());
				if (user == null)
					return SignOut("Logout");

				var document = await _documentRepository.GetWithUser(code, user.Id);

				return Ok(document);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
		// GET api/document/d/5
		[HttpGet("pdf/{code}")]
		public async Task<ActionResult?> GetPdf(string code)
		{
			try
			{
                var document = await _documentRepository.Get(code);

                var pdf = await _pdfService.GeneratePDF(document);

                if (pdf == null)
                    return null;
				

				string contentType = "application/octet-stream";
                
                return File(pdf, "application/pdf", code+".pdf");
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal Server Error");
			}
		}

		[HttpPost]
        [Authorize]
		public async Task<ActionResult<string>> Post([FromForm] AllUserDocumentDto doc)
		{
			try
			{
				var user = await _userRepository.GetByMail(_userService.GetMail());
				if (user == null)
					return SignOut("Logout");

				if (doc.Files == null || string.IsNullOrEmpty(doc.Recipients))
                    return BadRequest("File not exist");

				var recipients = JsonConvert.DeserializeObject<List<DocumentRecipientsDto>>(doc.Recipients);

                if(recipients == null || recipients.Count == 0)
					return BadRequest("No Recipient");

				var document = _documentService.CreateDocument(doc.Files, doc.Title, doc.Object, doc.Message, user, DocumentType.WithFlow);

                if(document == null)
					return BadRequest("error on document");

                var userDocuments = await _userDocumentService.CreateUserDocument(document, user, recipients);

                var a = await _userDocumentRepository.CreateRange(userDocuments);

				return Ok(document.Code);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal Server Error");
			}
		}

		[HttpPost("me")]
		[Authorize]
		public async Task<ActionResult<string>> PostMe([FromForm] AutoSignDocumentDto doc)
		{
			try
			{
				var user = await _userRepository.GetByMail(_userService.GetMail());
				if (user == null)
					return SignOut("Logout");

				if (doc.Files == null)
					return BadRequest("File not exist");

				var document = _documentService.CreateDocument(doc.Files, doc.Title!, "", "", user, DocumentType.WithoutFlow);

				if (document == null)
					return BadRequest("error on document");

				var userDocuments = await _userDocumentService.CreateUserDocument(document, user, new List<DocumentRecipientsDto>());

				await _userDocumentRepository.CreateRange(userDocuments);

                await _userDocumentRepository.UpdateSignAndParaphe(userDocuments.FirstOrDefault(), doc.Fields, doc.SignImage, doc.ParapheImage);

				return Ok(document.Code);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal Server Error");
			}
		}

		[HttpPut("sign/{code}")]
		[Authorize]
		public async Task<ActionResult<string>> Sign(string code, [FromForm] SignAndParaphe doc)
		{
			try
			{
				var user = await _userRepository.GetByMail(_userService.GetMail());
				if (user == null)
					return SignOut("Logout");

				bool isValidated = await _userDocumentRepository.UpdateSignAndParaphe(code, user.Id, doc.SignImage, doc.ParapheImage);
				if (isValidated)
					return Ok();
				else
					return BadRequest();
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal Server Error");
			}
		}

		[HttpPut("validate/{code}")]
		[Authorize]
		public async Task<ActionResult<string>> Validated(string code)
		{
			try
			{
				var user = await _userRepository.GetByMail(_userService.GetMail());
				if (user == null)
					return SignOut("Logout");

                bool isValidated = await _userDocumentRepository.Validate(code, user.Id);
                if (isValidated)
                    return Ok();
                else
                    return BadRequest();
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal Server Error");
			}
		}

		// DELETE api/<DocumentController>/5
		[HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(string code)
        {
            try
            {
                return Ok(await _documentRepository.Delete(code));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
