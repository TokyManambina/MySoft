using System.ComponentModel.DataAnnotations;

namespace SoftSignAPI.Model
{
	public class Flow
	{
		[Key]
		public Guid Id { get; set; }
		public string Name { get; set; }

		public virtual List<UserFlow> Users { get; set; }

	}
}
