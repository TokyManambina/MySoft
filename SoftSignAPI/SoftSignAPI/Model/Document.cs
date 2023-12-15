using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SoftSignAPI.Model
{
    public class Document
    {
        [Key]
        public string Code { get; set; }
        public string? DocPasword { get; set; }
        public string Filename { get; set; }
        public string Url { get; set; }
        public string? Title { get; set; }
        public string? Object { get; set; }
        public string? Message { get; set; }
        public DocumentType Type { get; set; }
        public DateTime DateSend { get; set; } = DateTime.Now;
        public DocumentStat Status { get; set; } = DocumentStat.Remaining;

        public virtual List<Attachement> Attachements { get; set; }
        public virtual List<UserDocument> UserDocuments { get; set; }
        public virtual List<DocumentDynamicField> DocumentDetailValues { get; set; }
        public virtual List<DocumentLink> DocumentLinks { get; set; }
    }
    public enum DocumentType
    {
		WithoutFlow, WithFlow
    }
    public enum DocumentStat
    {
        Remaining, Completed, Archived, Canceled
    }
}
