using System.ComponentModel.DataAnnotations;

namespace SoftSignAPI.Model
{
	public class DocumentDetail
	{
		[Key]
		public int Id{ get; set; }
		public string Label{ get; set; }
		public DetailType Type { get; set; }

		public virtual List<DocumentDetailValue>? Values { get; set; }
	}

	public enum DetailType
	{
		Text, Date, List, Checkbox
	}
}
