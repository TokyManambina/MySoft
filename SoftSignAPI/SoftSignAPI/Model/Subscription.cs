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
        public string? Location { get; set; }

        public Guid UserId { get; set; }
		[ForeignKey(nameof(UserId))]
		public virtual User User { get; set; }

        [ForeignKey(nameof(OfferId))]
        public int OfferId { get; set; }
        public virtual Offer Offer { get; set; }
    }
}
