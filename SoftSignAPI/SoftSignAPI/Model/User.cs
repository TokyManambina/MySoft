using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftSignAPI.Model
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Role Role { get; set; } = Role.User;
        public string? TransfertMail{ get; set; }

        public Guid? SocietyId{ get; set; }
		[ForeignKey(nameof(SocietyId))]
		public virtual Society? Society{ get; set; }

		public Guid? SubscriptionId { get; set; }
		[ForeignKey(nameof(SubscriptionId))]
		public virtual Subscription? Subscription { get; set; }

        public virtual List<UserDocument> UserDocuments { get; set; }


        public string? RefreshToken { get; set; } = string.Empty;
        public Nullable<DateTime> TokenCreated { get; set; }
        public Nullable<DateTime> TokenExpires { get; set; }


    }

    public enum Role
    {
		User, Controller, Admin
    }
}
