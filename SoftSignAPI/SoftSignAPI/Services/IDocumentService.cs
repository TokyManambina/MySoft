using SoftSignAPI.Dto;
using SoftSignAPI.Model;

namespace SoftSignAPI.Services
{
    public interface IDocumentService
    {
        Document? CreateDocument(IFormFile upload, User user);
    }
}
