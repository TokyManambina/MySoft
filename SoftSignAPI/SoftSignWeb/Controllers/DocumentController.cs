using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using SoftSignWeb.Models;

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

        // GET: DocumentController/Details/5
        [HttpGet]
		public async Task<ActionResult> GetMyDocument(string userId)
        {
            userId = "9ABDD01A-C4CA-44A7-99C5-32E9651D4BC0";
			var options = new RestClientOptions("https://localhost:7250")
			{
				MaxTimeout = -1,
			};
			var client = new RestClient(options);
			var request = new RestRequest("/api/Document/filter/posted", Method.Get);
            request.AddQueryParameter("userId", userId);
			//request.AddHeader("Cookie", "refreshToken=GZaFc4zWeWU2%2FgV4hwsT80SS2UWg1mbz1DDhIm6ZpgaAD0Ojy2IPv3Jy8ZEliYehHmDsAaZ9Nz606qqQN6erjw%3D%3D");
			RestResponse response = await client.ExecuteAsync(request);

			return Ok(response.Content);
        }

		[HttpGet]
		public async Task<ActionResult> GetDocumentInfo(string userId)
		{
			userId = "9ABDD01A-C4CA-44A7-99C5-32E9651D4BC0";
			var options = new RestClientOptions("https://localhost:7250")
			{
				MaxTimeout = -1,
			};
			var client = new RestClient(options);
			var request = new RestRequest("/api/Document/u/info", Method.Get);
			request.AddQueryParameter("userId", userId);
			//request.AddHeader("Cookie", "refreshToken=GZaFc4zWeWU2%2FgV4hwsT80SS2UWg1mbz1DDhIm6ZpgaAD0Ojy2IPv3Jy8ZEliYehHmDsAaZ9Nz606qqQN6erjw%3D%3D");
			RestResponse response = await client.ExecuteAsync(request);

			return Ok(response.Content);
		}

		// GET: DocumentController/Create
		public ActionResult Create()
        {
            return View();
        }

        // POST: DocumentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DocumentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DocumentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DocumentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DocumentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
