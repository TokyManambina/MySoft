using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftSignAPI.Model
{
    public class Subscription
    {
        [Key]
        public Guid Id { get; set; }
        public string Code { get; set; }

        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }
        public string? Location { get; set; }
        public int Capacity { get; set; }
        public int MaxUser { get; set; }
        public bool HasClientSpace { get; set; }
        public bool HasFlowManager { get; set; }
        public bool HasFlow { get; set; }
        public bool HasDynamicFieldManager { get; set; }
        public bool HasLibrary { get; set; }
        public bool HasPhysicalLibrary { get; set; }
        

		public virtual List<User> Users { get; set; }

        public virtual List<Flow> Flows { get; set; }
        public virtual List<DynamicField> DynamicFields { get; set; }
	}
}
