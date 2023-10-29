using SoftSignAPI.Model;

namespace SoftSignAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> Create(User user);
        Task<bool> Delete(Guid id);
        Task<User?> Get(Guid id);
        Task<List<User>?> GetAll(string? search = null, int? count = null, int? page = null);
        Task<User?> GetByMail(string mail);
        Task<bool> IsExist(Guid id);
        bool Save();
        Task<bool> Update(Guid id, User updateUser);
    }
}
