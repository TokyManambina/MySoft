using System.ComponentModel.DataAnnotations;

namespace SoftSignAPI.Model
{
	public class DocumentDetailValue
	{
		[Key]
		public string DocumentCode { get; set; }
		[Key]
		public int DocumentDetailId { get; set;}

		public string Value { get; set; }

		public virtual Document Document { get; set; }
		public virtual DocumentDetail DocumentDetail { get; set; }

	}
}
