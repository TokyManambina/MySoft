namespace SoftSignAPI.Dto
{
    public class UploadFileDto
    {
        public string? Object {  get; set; }
        public string? Message {  get; set; }
        public string? Cc {  get; set; }
        public IFormFile File {  get; set; }
        public List<FieldDto> field {  get; set; }
    }
}
