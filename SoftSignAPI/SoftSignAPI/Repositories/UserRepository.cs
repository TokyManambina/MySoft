using Microsoft.EntityFrameworkCore;
using SoftSignAPI.Context;
using SoftSignAPI.Interfaces;
using SoftSignAPI.Model;

namespace SoftSignAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly dbContext _db;

        public UserRepository(dbContext db)
        {
            _db = db;
        }

        public async Task<User?> Create(User user)
        {
            try
            {
                user = _db.Users.Add(user).Entity;
                await Save();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<bool> Update(Guid id, User updateUser)
        {
            try
            {
                User? user = await Get(id);
                if (user == null)
                    return false;
                user.Email = updateUser.Email;
                user.LastName = updateUser.LastName;
                user.FirstName = updateUser.FirstName;
                user.Role = updateUser.Role;
                user.TransfertMail = updateUser.TransfertMail;
                await Save();
                return true;

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
                User? user = await Get(id);
                if (user == null)
                    return false;
                _db.Remove(user);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<User?> Get(Guid id)
        {
            try
            {
                if (!await IsExist(id))
                    return null;
                return await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<User?> GetByMail(string mail)
        {
            try
            {
                return await _db.Users.FirstOrDefaultAsync(x => x.Email == mail);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<User>?> GetAll(string? search = null, int? count = null, int? page = null)
        {
            try
            {
                var query = _db.Users.AsQueryable();

                if (!string.IsNullOrEmpty(search))
                    query = query.Where(x => x.FirstName.ToLower().Contains(search.ToLower()) || x.LastName.ToLower().Contains(search.ToLower()) || x.Email.ToLower().Contains(search.ToLower()));

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
            return await _db.Users.AnyAsync(x => x.Id == id);
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
