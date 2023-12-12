using SoftSignAPI.Helpers;
using SoftSignAPI.Model;

namespace SoftSignAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Create(User user);
        Task<User?> Insert(string mail, string password);
        Task<bool> Delete(Guid id);
        Task<User?> Get(Guid id);
        Task<int> GetCountAll(int count);
        Task<List<User>?> GetAll(Guid? subscriptionId = null, string? search = null, int? count = null, int? page = null);
        Task<User?> GetByMail(string mail);
        Task<User?> GetByToken(string token);
        Task<bool> IsExist(Guid id);
        Task<bool> IsExist(string mail);
        bool Save();
        Task<bool> Update(Guid id, User updateUser);
        Task<bool> UpdateToken(Guid id, RefreshToken refreshToken);
    }
}
