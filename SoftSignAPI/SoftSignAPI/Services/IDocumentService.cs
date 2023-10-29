using SoftSignAPI.Dto;
using SoftSignAPI.Model;

namespace SoftSignAPI.Services
{
    public interface IDocumentService
    {
        Task<Document?> BuildDocument(UploadFileDto upload, string mail);
    }
}
