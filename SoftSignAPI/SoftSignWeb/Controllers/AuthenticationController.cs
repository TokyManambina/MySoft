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

        [HttpPost]
        [Route("/Auth")]
        public async Task<IActionResult> Login(string mail, string password)
        {
            try
            {
                var options = new RestClientOptions("https://localhost:7250")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/api/Authentication/login", Method.Post);
                request.AddHeader("Content-Type", "application/json");
            
                //request.AddHeader("Cookie", "refreshToken=GZaFc4zWeWU2%2FgV4hwsT80SS2UWg1mbz1DDhIm6ZpgaAD0Ojy2IPv3Jy8ZEliYehHmDsAaZ9Nz606qqQN6erjw%3D%3D");
                var body = JsonConvert.SerializeObject(new { email = mail, password = password });
                request.AddStringBody(body, DataFormat.Json);
                RestResponse response = await client.ExecuteAsync(request);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    return NotFound();

                var token = JsonConvert.DeserializeObject<Token>(response.Content);

                var coockieOption = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = token.Expires,
                };

                Response.Cookies.Append("refreshToken", token.refresh, coockieOption);

                return Ok(JsonConvert.SerializeObject(new
                {
                    type = "success",
                    statusCode = response.StatusCode,
                    uri = Url.Action("Index", "Document"),
                    token = token.token
                }));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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
