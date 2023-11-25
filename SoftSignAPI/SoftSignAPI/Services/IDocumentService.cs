using SoftSignAPI.Dto;
using SoftSignAPI.Model;

namespace SoftSignAPI.Services
{
    public interface IDocumentService
    {
        Task<Document?> BuildDocument(UploadFileDto upload, string mail);
		Task<Document?> CreateDocument(IFormFile upload, string mail);
	}
}
