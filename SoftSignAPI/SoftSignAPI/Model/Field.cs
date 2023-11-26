using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftSignAPI.Model
{
    public class Field
    {
        [Key]
        public int Id { get; set; }
        public string? Variable { get; set; }
        public string? Page { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public double? PDF_Width { get; set; }
        public double? PDF_Height { get; set; }
        public FieldType? FieldType { get; set; }
        public string? Detail{ get; set; }
        public string? Text{ get; set; }

        [ForeignKey(nameof(UserDocumentId))]
        public int UserDocumentId { get; set; }
        public virtual UserDocument UserDocument { get; set; }
    }

    public enum FieldType
    {
        Signature, Paraphe, Tampon, DateSign, Text
    }
}
