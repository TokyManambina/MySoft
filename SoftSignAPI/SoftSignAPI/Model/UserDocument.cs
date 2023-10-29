using System.ComponentModel.DataAnnotations.Schema;

namespace SoftSignAPI.Model
{
    public class UserDocument
    {
        public int Id { get; set; }

        [ForeignKey(nameof(UserId))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey(nameof(DocumentCode))]
        public string DocumentCode { get; set; }
        public virtual Document Document { get; set; }

        public int Step { get; set; }
        public string Role { get; set; }

        public bool IsFinished { get; set; }

        public virtual List<Field> Fields { get; set;}
    }

    public enum DocumentRole
    {
        Sender, Receiver, Validator, Signatory
    }
}
