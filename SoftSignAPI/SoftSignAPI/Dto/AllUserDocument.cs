using Newtonsoft.Json;

namespace SoftSignAPI.Dto
{
    public class AllUserDocument
    {
		//public double PDF_Width { get; set; }
		//public double PDF_Height { get; set; }
		public List<DocumentRecipientsDto>? Recipients { get; set; }
		public IFormFile? Files { get; set; }
        public DocumentDto? Document { get; set; }
    }
	public class AllUserDocumentDto
	{
		public double PDF_Width { get; set; }
		public double PDF_Height { get; set; }
		public string? Recipients { get; set; }
		public List<IFormFile>? Files { get; set; }
	}


}
