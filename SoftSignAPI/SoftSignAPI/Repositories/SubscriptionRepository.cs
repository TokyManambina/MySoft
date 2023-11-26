using Microsoft.EntityFrameworkCore;
using SoftSignAPI.Context;
using SoftSignAPI.Interfaces;
using SoftSignAPI.Model;

namespace SoftSignAPI.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly dbContext _db;

        public SubscriptionRepository(dbContext db)
        {
            _db = db;
        }

        public async Task<List<Subscription>?> GetAll(string? search = null, int? count = null, int? page = null)
        {
            try
            {
                var query = _db.Subscriptions.AsQueryable();

                if(!string.IsNullOrEmpty(search))
                    query = query.Where(x=>x.Code.ToLower().Contains(search.ToLower()));

                if (count != null && page != null)
                    return await query.Skip(count.Value * (page.Value - 1)).Take(count.Value).ToListAsync();

                if (count != null)
                    query = query.Skip(count.Value);
                if (page != null)
                    query = query.Take(page.Value);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Subscription?> Get(int id)
        {
            try
            {
                if (!await IsExist(id))
                    return null;

                return await _db.Subscriptions.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> IsExist(string code)
        {
            return await _db.Subscriptions.AnyAsync(x => x.Code == code);
        }
        public async Task<bool> IsExist(int id)
        {
            return await _db.Subscriptions.AnyAsync(x => x.Id == id);
        }
        public async Task<Subscription?> Create(Subscription subscription)
        {
            try
            {
                if (await IsExist(subscription.Id))
                    return null;

                subscription = _db.Subscriptions.Add(subscription).Entity;

                Save();

                return subscription;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> Update(int id, Subscription updateSubscription)
        {
            try
            {
                var subscription = await Get(id);

                if (subscription == null)
                    return false;

                subscription.Code = updateSubscription.Code;
                subscription.BeginDate = updateSubscription.BeginDate;
                subscription.EndDate = updateSubscription.EndDate;
                subscription.OfferId = updateSubscription.OfferId;


                _db.Subscriptions.Update(subscription);

                return Save();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var subscription = await Get(id);

                if (subscription == null)
                    return false;

                _db.Subscriptions.Remove(subscription);

                return Save();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Save()
        {
            try
            {
                return _db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
