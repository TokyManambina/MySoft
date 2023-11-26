﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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
        private readonly IUserRepository _userRepository;

        public DocumentController(IDocumentRepository documentRepository, IMapper mapper, IWebHostEnvironment hostingEnvironment, IDocumentService documentService, IUserService userService, IUserRepository userRepository)
        {
            _documentRepository = documentRepository;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _documentService = documentService;
            _userService = userService;
            _userRepository = userRepository;
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
        [HttpGet("filter/posted")]
        [Authorize]
        public async Task<ActionResult<List<ShowDocument>>> GetSenderDocument([FromQuery] string? search, [FromQuery] int? count, [FromQuery] int? page)
        {
            try
            {
                var user = await _userRepository.GetByMail(_userService.GetMail());
                if(user == null)
					return SignOut("Logout");

				return Ok(_mapper.Map<List<ShowDocument>?>(await _documentRepository.GetSenderDocument(userId: user.Id, search: search, count: count, page: page)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        // GET: api/<DocumentController>/filter/received?userId=
        [HttpGet("filter/received")]
        public async Task<ActionResult<List<ShowDocument>>> GetRecipientDocument([FromQuery] Guid? userId, [FromQuery] string? search, [FromQuery] int? count, [FromQuery] int? page)
        {
            try
            {
                return Ok(_mapper.Map<List<ShowDocument>?>(await _documentRepository.GetRecipientDocument(userId: userId, search: search, count: count, page: page)));
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
        public async Task<ActionResult<Document>> Get(string code)
        {
            try
            {
                return Ok(_mapper.Map<Document?>(await _documentRepository.Get(code)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // POST api/<DocumentController>
        //[HttpPost]
        //public async Task<ActionResult<string>> Post(IFormFile upload)
        //{
        //    try
        //    {
        //        var document = await _documentService.CreateDocument(upload, "test");

        //        return Ok(document.Code);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Internal Server Error");
        //    }
        //}

		[HttpPost]
		public async Task<ActionResult<string>> Post([FromForm] AllUserDocumentDto doc)
		{
			try
			{

				var recipients = JsonConvert.DeserializeObject<List<DocumentRecipientsDto>>(doc.Recipients);
				var fil = JsonConvert.DeserializeObject<IFormFile>(doc.Document);

				return Ok();
				//var document = await _documentService.CreateDocument(upload, "test");

				//return Ok(document.Code);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal Server Error");
			}
		}

		[HttpPost("{code}")]
        public ActionResult<string> Posts(string code, [FromBody] AllUserDocument list)
        {
            try
            {

                return Ok();
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
