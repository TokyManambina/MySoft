using SoftSignAPI.Model;

namespace SoftSignAPI.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<Subscription?> Create(Subscription subscription);
        Task<bool> Delete(int id);
        Task<Subscription?> Get(int id);
        Task<List<Subscription>?> GetAll(string? search = null, int? count = null, int? page = null);
        Task<bool> IsExist(string code);
        Task<bool> IsExist(int id);
        Task<bool> Save();
        Task<bool> Update(int id, Subscription updateSubscription);
    }
}
