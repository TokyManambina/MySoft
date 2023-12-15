using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftSignAPI.Model
{
	public class UserFlow
	{
		[Key]
		public Guid Id { get; set; }
		public string Mail { get; set; }
		public string Role { get; set; }
		public Guid FlowId { get; set; }

		[ForeignKey(nameof(FlowId))]
		public virtual Flow Flow { get; set; }
	}
}
