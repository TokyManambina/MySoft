using SoftSignAPI.Model;

namespace SoftSignAPI.Interfaces
{
    public interface IDocumentRepository
    {
        Task<Document?> Create(Document document);
        Task<bool> Delete(string code);
        Task<Document?> Get(string code);
        Task<List<Document>?> GetAll(string? search = null, int? count = null, int? page = null);
        Task<bool> IsExist(string code);
        Task<bool> Save();
        Task<bool> Update(string code, Document updateDocument);
    }
}
