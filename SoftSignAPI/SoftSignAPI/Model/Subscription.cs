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


        [ForeignKey(nameof(OfferId))]
        public int? OfferId { get; set; }
        public virtual Offer? Offer { get; set; }

		public virtual List<User> Users { get; set; }
	}
}
