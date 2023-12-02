using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftSignAPI.Model
{
	public class DocumentLink
	{
		[Key] 
		public string CodeLink { get; set; }
		public DateTime ExpiredDate { get; set; }
		public string CodeDocument { get; set; }

		[ForeignKey(nameof(CodeDocument))]
		public virtual Document Document { get; set; }

		//public virtual List<string>? Mails { get; set; }
	}
}
