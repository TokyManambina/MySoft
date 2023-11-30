using Microsoft.AspNetCore.Mvc;

namespace SoftSignWeb.Controllers
{
	public class ClientController : Controller
	{
		public IActionResult Dashboard()
		{
			return View();
		}
		public IActionResult UserManagement()
		{
			return View();
		}
	}
}
