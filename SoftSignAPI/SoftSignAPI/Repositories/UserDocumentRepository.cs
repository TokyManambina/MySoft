using Microsoft.EntityFrameworkCore;
using SoftSignAPI.Context;
using SoftSignAPI.Interfaces;
using SoftSignAPI.Model;
using System.Reflection.Metadata;

namespace SoftSignAPI.Repositories
{
    public class UserDocumentRepository : IUserDocumentRepository
    {
        private readonly dbContext _db;

        public UserDocumentRepository(dbContext db) 
        {  
            _db = db;   
        }

        public async Task<bool> IsExist(int id)
        {
            return await _db.UserDocuments.AnyAsync(x => x.Id == id);
        }
        public async Task<UserDocument?> Get(int? id = null, Guid? userId = null, string? code = null)
        {
            try
            {
                var query = _db.UserDocuments.AsQueryable();

                if (query == null)
                    return null;

                if(id != null)
                    return await query.FirstOrDefaultAsync(x => x.Id == id);

                if(userId != null)
                    query = query.Where(x=>x.UserId == userId);

                if (code != null)
                    query = query.Where(x => x.DocumentCode == code);

                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<UserDocument>?> GetAll(Guid? userId = null, string? code = null, int? count = null, int? page = null)
        {
            try
            {
                var query = _db.UserDocuments.AsQueryable();

                if(userId != null)
                    query = query.Where(x=>x.UserId == userId);

                if (!string.IsNullOrEmpty(code))
                    query = query.Where(x => x.DocumentCode == code);

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
        public async Task<UserDocument?> Create(UserDocument newUserDocument)
        {
            try
            {
                if (await IsExist(newUserDocument.Id))
                    return null;

                newUserDocument = _db.UserDocuments.Add(newUserDocument).Entity;
                await Save();

                return newUserDocument;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> Update(int id, UserDocument updateUserDocument)
        {
            try
            {
                var userDocument = await Get(id);

                if (userDocument == null)
                    return false;

                userDocument.IsFinished = updateUserDocument.IsFinished;

                _db.UserDocuments.Update(userDocument);

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
                var userDocument = await Get(id);

                if (userDocument == null)
                    return false;

                _db.UserDocuments.Remove(userDocument);

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
