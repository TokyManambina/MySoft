using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using SoftSignWeb.Models;

namespace SoftSignWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IConfiguration _config;

        public UserController(ILogger<AuthenticationController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [Route("/liste")]
        public IActionResult Liste()
        {
            return View();
        }
    }
}
