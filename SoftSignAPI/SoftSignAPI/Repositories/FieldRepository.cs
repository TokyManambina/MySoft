using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SoftSignAPI.Context;
using SoftSignAPI.Interfaces;
using SoftSignAPI.Model;

namespace SoftSignAPI.Repositories
{
    public class FieldRepository : IFieldRepository
    {
        private readonly dbContext _db;

        public FieldRepository(dbContext db)
        {
            _db = db;
        }

        public async Task<bool> IsExist(int id)
        {
            return await _db.Fields.AnyAsync(x => x.Id == id);
        }
        public async Task<Field?> Get(int id)
        {
            try
            {
                if (!await IsExist(id))
                    return null;

                return await _db.Fields.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<Field>?> GetAll(int? userDocumentId = null, int? count = null, int? page = null)
        {
            try
            {
                var query = _db.Fields.AsQueryable();

                if (userDocumentId != null)
                    query = query.Where(x => x.UserDocumentId == userDocumentId);

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
        public async Task<Field?> Create(Field field)
        {
            try
            {
                if (await IsExist(field.Id))
                    return null;

                field = _db.Fields.Add(field).Entity;

                await Save();

                return field;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> Update(int id, Field updateField)
        {
            try
            {
                var field = await Get(id);

                if (field == null)
                    return false;

                field.Text = updateField.Text;
                field.Detail = updateField.Detail;

                _db.Fields.Update(field);

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
                var field = await Get(id);

                if (field == null)
                    return false;

                _db.Fields.Remove(field);

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
