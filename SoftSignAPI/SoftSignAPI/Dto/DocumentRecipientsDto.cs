using SoftSignAPI.Model;
using System.Drawing;

namespace SoftSignAPI.Dto
{
	public class DocumentRecipientsDto
	{
		public string Role{ get; set; }
		public string Mail{ get; set; }
		public string? Cc{ get; set; }
		public string? Message{ get; set; }
		public string Color{ get; set; }
		public List<FieldDto> Fields{ get; set; }
		
	}
}
