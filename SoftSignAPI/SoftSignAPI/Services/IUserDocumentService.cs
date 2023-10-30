using SoftSignAPI.Dto;

namespace SoftSignAPI.Services
{
    public interface IUserDocumentService
    {
        Task LinkUserWithDocument(string code, List<UserRoleDocumentDto> users);
    }
}
