using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using SoftSignWeb.Models;

namespace SoftSignWeb.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IConfiguration _config;

        public AuthenticationController(ILogger<AuthenticationController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [Route("/")]
        public IActionResult Login()
        {
            return View();
            return RedirectToAction("Index", "Home");
        }

		[HttpGet]
		[Route("/Auth")]
		public JsonResult Authenticate()
		{
			return Json(new
			{
				type = "success",
				baseUrl = Url.Action(""),
				url = Url.Action("Index", "Document")
			});
		}

        [HttpGet]
        [Route("/Disconnect")]
        public IActionResult Disconnect()
        {
            
            return RedirectToAction("");
        }


        [HttpPost]
        [Route("/Register")]
        public ActionResult Register(string mail, string password)
        {
            return View();
        }
    }
}
