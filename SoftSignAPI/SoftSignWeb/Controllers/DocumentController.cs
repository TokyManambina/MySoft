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
        public async Task<ActionResult> Index()
        {
            return View();
        }

        public ActionResult NewDocument()
        {
            return View();
        }
	}
}
