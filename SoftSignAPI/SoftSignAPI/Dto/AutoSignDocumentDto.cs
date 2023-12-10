using Newtonsoft.Json;

namespace SoftSignAPI.Dto
{
	public class AutoSignDocumentDto
	{
		public string Object { get; set; }
		public string Message { get; set; }
		public string? Fields { get; set; }
		public IFormFile? Files { get; set; }
		public byte[] Sign{ get; set; }
		public List<IFormFile>? PJ { get; set; }
	}


}
