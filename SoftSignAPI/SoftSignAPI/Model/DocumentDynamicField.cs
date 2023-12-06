using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftSignAPI.Model
{
	public class DocumentDynamicField
	{
		[Key]
		public string DocumentCode { get; set; }
		[Key]
		public Guid DocumentDetailId { get; set;}

		public string Value { get; set; }


		[ForeignKey(nameof(DocumentCode))]
		public virtual Document Document { get; set; }
		[ForeignKey(nameof(DocumentDetailId))]
		public virtual DynamicField DocumentDetail { get; set; }

	}
}
