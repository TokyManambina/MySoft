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
		Task<List<ShowDocument>?> GetOwnerDocument(User user, string? search = null, int? count = null, int? page = null);
		Task<List<ShowDocument>?> GetReceivedDocument(User user, string? search = null, int? count = null, int? page = null);
		Task<List<ShowDocument>?> GetSendedDocument(User user , string? search = null, int? count = null, int? page = null);
		Task<List<ShowDocument>?> GetDocuments(User user, DocumentStat? stat = null, string ? search = null, int? count = null, int? page = null);
		Task<bool> IsExist(string code);
        bool Save();
        Task<bool> Update(string code, Document updateDocument);
    }
}
