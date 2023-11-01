namespace SoftSignAPI.Dto
{
    public class UserDocumentDto
    {
        public int? Id { get; set; }
        public Guid? UserId { get; set; }
        public string? DocumentCode { get; set; }
        public int? Step { get; set; }
        public string? Role { get; set; }
        public bool? IsFinished { get; set; }
    }
}
