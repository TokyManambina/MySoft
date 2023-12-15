using SoftSignAPI.Model;

namespace SoftSignAPI.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<Subscription?> Create(Subscription subscription);
        Task<bool> Delete(Guid id);
        Task<Subscription?> Get(Guid id);
        Task<List<Subscription>?> GetAll(string? search = null, int? count = null, int? page = null);
        Task<bool> IsExist(string code);
        Task<bool> IsExist(Guid id);
        bool Save();
        Task<bool> Update(Guid id, Subscription updateSubscription);
    }
}
