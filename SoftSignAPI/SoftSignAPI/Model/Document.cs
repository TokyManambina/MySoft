using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SoftSignAPI.Model
{
    public class Document
    {
        [Key]
        public string Code { get; set; }
        public string? DocPasword { get; set; }
        public required string[] Filenames { get; set; }
        public required string Location { get; set; }
        public required DateTime DateSend { get; set; }
        public DocumentStat Status { get; set; }
        public PriorityLevel Priority { get; set; }
        public string? Detail { get;set; }


        public required virtual List<UserDocument> UserDocuments { get; set; }
    }
    public enum DocumentStat
    {
        Remaining, Completed, Archived, Canceled
    }
    public enum PriorityLevel
    {
        NonUrgent, Normal, Important, Urgent, VeryUrgent
    }
}
