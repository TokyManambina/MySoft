using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftSignAPI.Model
{
	public class DynamicField
	{
		[Key]
		public Guid Id{ get; set; }
		public string Label{ get; set; }
		public bool isRequired{ get; set; }
		public DetailType Type { get; set; }
		public Guid SubscriptionId { get; set; }

		[ForeignKey(nameof(SubscriptionId))]
		public virtual Subscription Subscription { get; set; }

		public virtual List<DynamicFieldItem>? Items{ get; set; }
		public virtual List<DocumentDynamicField>? Values { get; set; }
	}

	public enum DetailType
	{
		Text, Date, List, CheckBox, RadioBox
	}
}
