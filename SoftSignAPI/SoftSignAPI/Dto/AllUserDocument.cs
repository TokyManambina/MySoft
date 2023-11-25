namespace SoftSignAPI.Dto
{
	public class AllUserDocument
	{
		public List<DocumentRecipientsDto> Recipients { get; set; }
		public double PDF_Width { get; set; }
		public double PDF_Height { get; set; }
	}
}
