using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftSignAPI.Model
{
	public class DynamicFieldItem
	{
		[Key]
		public Guid Id{ get; set; }
		public string Value { get; set; }

		public Guid DetailId { get; set; }

		[ForeignKey(nameof(DetailId))]
		public virtual DynamicField Detail { get; set; }
	}
}
