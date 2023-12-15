using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SoftSignAPI.Context;
using SoftSignAPI.Helpers;
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

        public async Task<User> Create(User user)
        {
            try
            {
                user.Email = user.Email.ToLower();
                var u = await _db.Users.AddAsync(user);
                Save();
                return u.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
		public async Task<User?> Insert(string mail, string password, Guid? societeId, Guid? SubscriptionId)
		{
			try
			{
                mail = mail.ToLower();
				if (await IsExist(mail))
					return null;

				bool noPassword = false;

				if (string.IsNullOrEmpty(password))
				{
					noPassword = true;
					password = Tools.RandomPassword(mail);
				}

				var newuser = _db.Users.Add(new User()
				{
					Email = mail,
					Password = BCrypt.Net.BCrypt.HashPassword(password),
                    SubscriptionId = SubscriptionId,
                    SocietyId = societeId,

                });

                				
				await _db.SaveChangesAsync();

				return newuser.Entity;
			}
			catch (Exception ex)
			{
				return null;
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

                return Save();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> UpdateToken(Guid id, RefreshToken refreshToken)
        {
            try
            {
                var user = await Get(id);
                if (user == null)
                    return false;
               
                user.RefreshToken = refreshToken.Token;
                user.TokenCreated = refreshToken.Created;
                user.TokenExpires = refreshToken.Expires;
                _db.Users.Update(user);

                /*var rows = await _db.Users.Where(x => x.Id == id).ExecuteUpdateAsync(x => x
                    .SetProperty(u => u.RefreshToken ,refreshToken.Token)
                    .SetProperty(u => u.TokenCreated , refreshToken.Created)
                    .SetProperty(u => u.TokenExpires , refreshToken.Expires)
                );*/

                _db.SaveChanges();
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

                return Save();
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
                return await _db.Users.Include(x => x.Society).Include(x => x.Subscription).FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<int> GetCountAll(int count)
        {
            try
            {
                return await _db.Users.CountAsync()/count;
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
                return await _db.Users.Include(x=>x.Society).Include(x=>x.Subscription).FirstOrDefaultAsync(x => x.Email == mail);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<User?> GetByToken(string token)
        {
            try
            {
                return await _db.Users.FirstOrDefaultAsync(x => x.RefreshToken == token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<User>?> GetAll(Guid? subscriptionId = null, string? search = null, int? count = null, int? page = null)
        {
            try
            {
                var query = _db.Users.AsQueryable();

                if(subscriptionId != null)
                    query = query.Where(x=>x.SubscriptionId ==  subscriptionId);

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
        public async Task<bool> IsExist(string mail)
        {
            return await _db.Users.AnyAsync(x => x.Email == mail.ToLower());
        }
        public bool Save()
        {
            try
            {
                return _db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }

        }
    }
}
