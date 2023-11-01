using SoftSignAPI.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftSignAPI.Dto
{
	public class DocumentByUserDto
	{
		public int? Id { get; set; }
		public Guid? UserId { get; set; }
		public string UserEmail{ get; set; }
		public string? DocumentCode { get; set; }
		public DocumentDto? Document { get; set; }

		public int? Step { get; set; } = 0;
		public string? Role { get; set; } = string.Empty;
		public string? Message { get; set; }
		public string? Cc { get; set; }

		public bool? IsFinished { get; set; } = false;
	}
}
