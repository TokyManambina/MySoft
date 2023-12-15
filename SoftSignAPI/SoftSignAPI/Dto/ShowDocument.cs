using SoftSignAPI.Model;

namespace SoftSignAPI.Dto
{
	public class ShowDocument
	{
		public string? Code { get; set; }
		public string? Object { get; set; }
		public string? Message { get; set; }
		public DateTime? DateSend { get; set; }
		public DocumentStat? Status { get; set; }
	}
}
