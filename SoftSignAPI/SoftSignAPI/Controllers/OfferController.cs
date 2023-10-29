using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SoftSignAPI.Dto;
using SoftSignAPI.Interfaces;
using SoftSignAPI.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftSignAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IOfferRepository _offerRepository;
        private readonly IMapper _mapper;

        public OfferController(IOfferRepository offerRepository, IMapper mapper)
        {
            _offerRepository = offerRepository;
            _mapper = mapper;
        }

        // GET: api/<OfferController>
        [HttpGet]
        public ActionResult<List<OfferDto>?> Get([FromQuery] bool? isActive, [FromQuery] int? count, [FromQuery] int? page)
        {
            try
            {
                return Ok(_mapper.Map<List<OfferDto>?>(_offerRepository.GetAll(isActive: isActive,count: count, page: page)));
            }catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET: api/<OfferController>/find
        [HttpGet("{find}")]
        public ActionResult<List<OfferDto>?> Get([FromQuery] string search,[FromQuery] int? count, [FromQuery] int? page)
        {
            try
            {
                return Ok(_mapper.Map<List<OfferDto>?>(_offerRepository.GetAll(search: search,count: count, page: page)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET api/<OfferController>/5
        [HttpGet("{id}")]
        public ActionResult<OfferDto?> Get(int id)
        {
            try
            {
                return Ok(_mapper.Map<OfferDto?>(_offerRepository.Get(id)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // POST api/<OfferController>
        [HttpPost]
        public ActionResult Post([FromBody] Offer newOffer)
        {
            try
            {
                return Ok(_offerRepository.Create(newOffer));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // PUT api/<OfferController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Offer updateOffer)
        {
            try
            {
                return Ok(_offerRepository.Update(id, updateOffer));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // DELETE api/<OfferController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                return Ok(_offerRepository.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
