using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using SoftSignWeb.Models;
using System.Reflection;

namespace SoftSignWeb.Controllers
{
    public class DocumentController : Controller
    {
        // GET: DocumentController
        [Route("documents")]
        public async Task<ActionResult> Index()
        {
            return View();
        }
		[Route("document/{code}")]
		public async Task<ActionResult> Detail()
		{
			return View();
		}
        [Route("newDocument")]
		public async Task<ActionResult> NewDocument()
        {
            return View();
        }
	}
}
