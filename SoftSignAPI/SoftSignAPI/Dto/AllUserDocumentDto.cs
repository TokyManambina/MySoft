using Newtonsoft.Json;

namespace SoftSignAPI.Dto
{
	public class AllUserDocumentDto
	{
		public string Title { get; set; }
		public string Object { get; set; }
		public string Message { get; set; }
		public string? Recipients { get; set; }
		public IFormFile? Files { get; set; }
		public List<IFormFile>? PJ { get; set; }
	}


}
