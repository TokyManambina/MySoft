using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftSignAPI.Model
{
	public class Attachement
	{
		[Key] 
		public Guid Id { get; set; }
		public string Filename { get; set; }
		public string Url { get; set; }
		public string DocumentCode { get; set; }

		[ForeignKey(nameof(DocumentCode))]
		public virtual Document Document { get; set; }
	}
}
