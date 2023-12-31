﻿using System.ComponentModel.DataAnnotations.Schema;

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
        public string? Role { get; set; }
        public string? Color { get; set; }
        public string? Message { get; set; }
        public byte[]? Signature { get; set; }
        public byte[]? Paraphe { get; set; }

        public bool MyTurn { get; set; } = false;
        public bool IsFinished { get; set; } = false;

        public virtual List<Field> Fields { get; set;}
    }

    public enum DocumentRole
    {
        Sender, Recipient, Receiver, Validator, Signatory, 
    }
}
