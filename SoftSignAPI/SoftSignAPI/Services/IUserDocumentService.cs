using SoftSignAPI.Dto;
using SoftSignAPI.Model;

namespace SoftSignAPI.Services
{
    public interface IUserDocumentService
    {
        Task LinkUserWithDocument(string code, List<UserRoleDocumentDto> users);
        Task<List<UserDocument>> CreateUserDocument(Document document, User user, List<DocumentRecipientsDto> ListRecipient);

	}
}
