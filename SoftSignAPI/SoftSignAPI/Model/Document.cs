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
        public string? Cc { get; set; }
        public string Object { get; set; }
        public string Message { get; set; }
        public DateTime DateSend { get; set; }
        public DocumentStat Status { get; set; }

        public virtual List<UserDocument> UserDocuments { get; set; }
    }
    public enum DocumentStat
    {
        Remaining, Completed, Archived, Canceled
    }
}
