using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SoftSignAPI.Interfaces;
using SoftSignAPI.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftSignAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IRepository<Offer> _offerRepository;
        private readonly IMapper _mapper;

        public OfferController(IRepository<Offer> offerRepository, IMapper mapper)
        {
            _offerRepository = offerRepository;
            _mapper = mapper;
        }

        // GET: api/<OfferController>
        [HttpGet]
        public ActionResult<List<Offer>?> Get([FromQuery] int? count, [FromQuery] int? page)
        {
            try
            {
                return Ok(_mapper.Map<List<Offer>?>(_offerRepository.GetAll(count: count, page: page)));
            }catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET api/<OfferController>/5
        [HttpGet("{id}")]
        public ActionResult<Offer?> Get(int id)
        {
            try
            {
                return Ok(_mapper.Map<Offer?>(_offerRepository.Get(id)));
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
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OfferController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
