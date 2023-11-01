using SoftSignAPI.Dto;
using SoftSignAPI.Model;

namespace SoftSignAPI.Interfaces
{
    public interface IDocumentRepository
    {
        Task<Document?> Create(Document document);
        Task<bool> Delete(string code);
        Task<Document?> Get(string code);
        Task<List<Document>> GetAll(string? search = null, int? count = null, int? page = null);
		Task<List<DocInfo>?> GetDocumentInfo(Guid userId);
		Task<List<Document>?> GetRecipientDocument(Guid? userId = null, string? search = null, int? count = null, int? page = null);
		Task<List<Document>?> GetSenderDocument(Guid? userId = null, string? search = null, int? count = null, int? page = null);
		Task<bool> IsExist(string code);
        bool Save();
        Task<bool> Update(string code, Document updateDocument);
    }
}
