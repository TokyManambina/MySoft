using Newtonsoft.Json;

namespace SoftSignAPI.Dto
{
	public class AutoSignDocumentDto
	{
		public string? Title { get; set; }
		public string? Fields { get; set; }
		public IFormFile? Files { get; set; }
		public string? SignImage{ get; set; }
		public string? ParapheImage { get; set; }
		public List<IFormFile>? PJ { get; set; }
	}


}
