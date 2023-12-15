namespace SoftSignAPI.Dto
{
	public class UploadDocument
	{
		public List<IFormFile> File { get; set; }
		public List<FieldDto> field { get; set; }
	}
}
