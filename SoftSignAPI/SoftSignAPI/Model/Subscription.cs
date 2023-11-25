using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace SoftSignAPI.Model
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }
        public required string Code { get; set; }
        public required string Repository { get; set; }

        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        public int Capacity { get; set; }
        public int UserMax { get; set; }
        public int? DocumentMax { get; set; }

        public bool IsSociety { get; set; } = false;
        public bool CanAdministrate { get; set; } = false;

        public bool HasLibrary { get; set; } = false;
        public bool HasFlow { get; set; } = false;
        public bool HasFlowManager { get; set; } = false;
        public bool MultipleDocument { get; set; } = false;

        public int OfferId { get; set; }
        [ForeignKey(nameof(OfferId))]
        public required virtual Offer Offer { get; set; }

        public virtual List<User>? Users { get; set; }
    }
}
