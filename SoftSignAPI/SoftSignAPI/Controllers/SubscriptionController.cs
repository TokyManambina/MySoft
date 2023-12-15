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
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _UserService;



        public SubscriptionController(IMapper mapper, IUserService userService, IUserRepository userRepository, ISubscriptionRepository subscriptionRepository)
        {
            _mapper = mapper;
            _subscriptionRepository = subscriptionRepository;
            _UserService = userService;
            _userRepository = userRepository;
        }

        // GET: api/<userController>
        [HttpGet("get")]
        public async Task<ActionResult<List<SubscriptionDto>>> Get()
        {
            try
            {
                var user=await _userRepository.GetByMail(_UserService.GetMail());
                var liste = await _subscriptionRepository.GetAll();
                var subscriptionId = user?.SubscriptionId;
                var lista = subscriptionId== null ? liste : liste?.Where(u => u.Id == subscriptionId) ;
                return Ok(_mapper.Map<List<SubscriptionDto>?>(lista));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
