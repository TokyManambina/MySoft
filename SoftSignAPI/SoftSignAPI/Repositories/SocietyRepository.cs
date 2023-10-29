using Microsoft.EntityFrameworkCore;
using SoftSignAPI.Context;
using SoftSignAPI.Interfaces;
using SoftSignAPI.Model;

namespace SoftSignAPI.Repositories
{
    public class SocietyRepository : IRepository<Society>
    {
        private readonly dbContext _db;

        public SocietyRepository(dbContext db)
        {
            _db = db;
        }

        public async Task<List<Society>?> GetAll(object? id = null, int? count = null, int? page = null)
        {
            try
            {
                var query = _db.Societies.AsQueryable();

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

        public async Task<Offer?> Get(object id)
        {
            try
            {
                if (!await IsExist(id))
                    return null;

                return await _db.Offers.FirstOrDefaultAsync(x => x.Id == (int)id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> IsExist(string name)
        {
            return await _db.Offers.AnyAsync(x => x.Name == name);
        }

        public async Task<bool> IsExist(object id)
        {
            return await _db.Offers.AnyAsync(x => x.Id == (int)id);
        }

        public async Task<Offer?> Create(Offer offer)
        {
            try
            {
                if (await IsExist(offer.Name))
                    return null;

                offer = _db.Offers.Add(offer).Entity;

                await Save();

                return offer;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(object id, Offer updateOffer)
        {
            try
            {
                var offer = await Get(id);

                if (offer == null)
                    return false;

                offer.Name = updateOffer.Name;
                offer.Description = updateOffer.Description;
                offer.Hour = updateOffer.Hour;
                offer.Hour = updateOffer.Hour;
                offer.Hour = updateOffer.Hour;
                offer.Hour = updateOffer.Hour;

                _db.Offers.Update(offer);

                return await Save();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(object id)
        {
            try
            {
                var offer = await Get(id);

                if (offer == null)
                    return false;

                _db.Remove(offer);

                return await Save();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<bool> Save()
        {
            try
            {
                return await _db.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
