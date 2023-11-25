using System.ComponentModel.DataAnnotations.Schema;

namespace SoftSignAPI.Model
{
    public class UserDocument
    {
        public int Id { get; set; }
        public required Guid UserId { get; set; }
        public required string DocumentCode { get; set; }

        public string Role { get; set; }
        public string Color { get; set; }
        public string? Message { get; set; }

        public int Step { get; set; } = 0;
        public bool MyTurn { get; set; } = false;
        public bool IsFinished { get; set; } = false;


        [ForeignKey(nameof(UserId))]
        public required virtual User User { get; set; }
        [ForeignKey(nameof(DocumentCode))]
        public required virtual Document Document { get; set; }
        public virtual List<Field>? Fields { get; set;}
    }

    public enum DocumentRole
    {
        Sender, Recipient, Receiver, Validator, Signatory
    }
}
