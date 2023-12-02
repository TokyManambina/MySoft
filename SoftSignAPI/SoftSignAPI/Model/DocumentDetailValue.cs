using System.ComponentModel.DataAnnotations;

namespace SoftSignAPI.Model
{
	public class DocumentDetailValue
	{
		[Key]
		public int Id{ get; set; }
		public string Value { get; set; }
	}
}
