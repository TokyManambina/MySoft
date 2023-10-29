using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftSignAPI.Model
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }

        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        [ForeignKey(nameof(SocietyId))]
        public Guid SocietyId { get; set; }
        public virtual Society Society { get; set; }

        [ForeignKey(nameof(OfferId))]
        public int OfferId { get; set; }
        public virtual Offer Offer { get; set; }
    }
}
