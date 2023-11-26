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
        public Role Role{ get; set; }
        public string? TransfertMail{ get; set; }

        [ForeignKey(nameof(SocietyId))]
        public Guid? SocietyId{ get; set; }
        public virtual Society? Society{ get; set; }

        public virtual List<UserDocument> UserDocuments { get; set; }
        public virtual List<Subscription> Subscriptions { get; set; }


        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }


    }

    public enum Role
    {
        User, Admin, sa, Commercial, Controller
    }
}
