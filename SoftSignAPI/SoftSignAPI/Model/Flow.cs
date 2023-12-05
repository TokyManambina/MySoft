using System.ComponentModel.DataAnnotations;

namespace SoftSignAPI.Model
{
	public class Flow
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }

	}
}
