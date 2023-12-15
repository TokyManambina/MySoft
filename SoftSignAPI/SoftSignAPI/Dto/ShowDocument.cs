using SoftSignAPI.Model;

namespace SoftSignAPI.Dto
{
	public class ShowDocument
	{
		public string? Code { get; set; }
		public string? Object { get; set; }
		public string? Title { get; set; }
		public string? Message { get; set; }
		public string? De { get; set; }
		public string? Pour { get; set; }
		public DateTime? DateSend { get; set; }
		public DocumentStat? Status { get; set; }
	}
}
