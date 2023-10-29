using Microsoft.EntityFrameworkCore;
using SoftSignAPI.Context;
using SoftSignAPI.Interfaces;
using SoftSignAPI.Model;

namespace SoftSignAPI.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly dbContext _db;

        public OfferRepository(dbContext db)
        {
            _db = db;
        }

        public async Task<List<Offer>?> GetAll(string? search = null, bool? isActive = null, int? count = null, int? page = null)
        {
            try
            {
                var query = _db.Offers.AsQueryable();

                if (isActive != null)
                    query = query.Where(x => x.IsActive == isActive);

                if(!string.IsNullOrEmpty(search))
                    query = query.Where(x => x.Name.ToLower().Contains(search.ToLower()) || x.Code.ToLower().Contains(search.ToLower()));

                if (count != null && page != null)
                    return await query.Skip(count.Value * (page.Value - 1)).Take(count.Value).ToListAsync();

                if (count != null)
                    query = query.Skip(count.Value);
                if (page != null)
                    query = query.Take(page.Value);

                return await query.ToListAsync();
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }
        public async Task<Offer?> Get(int id)
        {
            try
            {
                if (!await IsExist(id))
                    return null;

                return await _db.Offers.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Offer?> Get(string code)
        {
            try
            {
                if (!await IsExist(code))
                    return null;

                return await _db.Offers.FirstOrDefaultAsync(x => x.Code == code);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> IsExist(string code)
        {
            return await _db.Offers.AnyAsync(x => x.Code == code);
        }
        public async Task<bool> IsExist(int id)
        {
            return await _db.Offers.AnyAsync(x => x.Id == id);
        }
        public async Task<Offer?> Create(Offer offer)
        {
            try
            {
                if (await IsExist(offer.Code))
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
        public async Task<bool> Update(int id, Offer updateOffer)
        {
            try
            {
                var offer = await Get(id);

                if (offer == null)
                    return false;

                offer.Code = updateOffer.Code;
                offer.Name = updateOffer.Name;
                offer.Description = updateOffer.Description;
                offer.Hour = updateOffer.Hour;
                offer.Day = updateOffer.Day;
                offer.Month = updateOffer.Month;
                offer.Year = updateOffer.Year;
                offer.Description = updateOffer.Description;
                offer.Price = updateOffer.Price;
                offer.IsActive = updateOffer.IsActive;

                _db.Offers.Update(offer);

                return await Save();

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
                var offer = await Get(id);

                if (offer == null)
                    return false;

                _db.Offers.Remove(offer);

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
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
