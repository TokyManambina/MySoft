using SoftSignAPI.Dto;
using SoftSignAPI.Model;

namespace SoftSignAPI.Services
{
    public interface IDocumentService
    {
        Document? CreateDocument(IFormFile upload, string Title, string Object, string Message,User user);
    }
}
