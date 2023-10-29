using Microsoft.EntityFrameworkCore;
using SoftSignAPI.Context;
using SoftSignAPI.Interfaces;
using SoftSignAPI.Model;
using System.Net.Sockets;

namespace SoftSignAPI.Repositories
{
    public class SocietyRepository : ISocietyRepository
    {
        private readonly dbContext _db;

        public SocietyRepository(dbContext db)
        {
            _db = db;
        }

        public async Task<Society?> Create(Society society)
        {
            try
            {
                society = _db.Societies.Add(society).Entity;
                Save();
                return society;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> Update(Guid id, Society updateSociety)
        {
            try
            {
                Society? society = await Get(id);
                if (society == null)
                    return false;
                society.Name = updateSociety.Name;
                society.Storage = updateSociety.Storage;

                return Save();
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> Delete(Guid id)
        {
            try
            {
                Society? society = await Get(id);
                if (society == null)
                    return false;
                _db.Remove(society);

                return Save();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Society?> Get(Guid id)
        {
            try
            {
                if (!await IsExist(id))
                    return null;
                return await _db.Societies.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Society?> GetByUser(string mail)
        {
            try
            {
                return await _db.Users.Where(x=>x.Email == mail).Include(x=>x.Society).Join(_db.Societies, x=>x.SocietyId, y=>y.Id, (x,y) => y).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<Society>?> GetAll(string? search = null, int? count = null, int? page = null)
        {
            try
            {
                var query = _db.Societies.AsQueryable();

                if (!string.IsNullOrEmpty(search))
                    query = query.Where(x => x.Name.ToLower().Contains(search.ToLower()));

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
        public async Task<bool> IsExist(Guid id)
        {
            return await _db.Societies.AnyAsync(x => x.Id == id);
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
