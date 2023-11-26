using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SoftSignAPI.Dto;
using SoftSignAPI.Helpers;
using SoftSignAPI.Interfaces;
using SoftSignAPI.Model;
using SoftSignAPI.Repositories;
using SoftSignAPI.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftSignAPI.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        const Role e = Role.User;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        
        public AuthenticationController(ILogger<AuthenticationController> logger, IMapper mapper, IUserRepository userRepository, ITokenService tokenService)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("register")]
        //[Authorize(Roles = $"{nameof(Role.Commercial)}, {nameof(Role.sa)}" )]
        public async Task<ActionResult<User>> Register(AuthenticationDto request)
        {
            try
            {
                if (await _userRepository.IsExist(request.Email))
                    return Conflict("User already exist.");

                request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

                if (await _userRepository.Create(_mapper.Map<User>(request)) == null)
                    return StatusCode(500, "Internal Server Error");

                return Ok("User created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login([FromBody] AuthenticationDto request)
        {
            try
            {
                var user = await _userRepository.GetByMail(request.Email);

                if (user == null)
                    return NotFound("User not found.");

                if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                    return Unauthorized("Wrong password.");

                string token = _tokenService.CreateToken(user); 
                var refresh = await _tokenService.SetRefreshToken(user, Response);

                return Ok(new {
                    token = token,
                    refresh = refresh.Token,
                    Created = refresh.Created,
                    Expires = refresh.Expires
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        [Route("refreshToken")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> RefreshTokens()
        {

            var refreshToken = Request.Cookies["refreshToken"];
            if(refreshToken == null)
                return Unauthorized();
            var user = await _userRepository.GetByToken(refreshToken);

            if (user == null)
                return NotFound("User not found");

            if (!user.RefreshToken.Equals(refreshToken))
                return Unauthorized("Invalid Refresh Token.");

            if (user.TokenExpires < DateTime.Now)
                return Unauthorized("Token Expired");

            string token = _tokenService.CreateToken(user);
            _tokenService.SetRefreshToken(user, Response);

            return Ok(token);
        }

    }
}
