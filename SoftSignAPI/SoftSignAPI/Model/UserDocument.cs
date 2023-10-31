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

        public int Step { get; set; } = 0;
        public string Role { get; set; } = string.Empty;
        public string? Message { get; set; }
        public string? Cc { get; set; }

        public bool IsFinished { get; set; } = false;

        public virtual List<Field> Fields { get; set;}
    }

    public enum DocumentRole
    {
        Sender, Receiver, Validator, Signatory
    }
}
