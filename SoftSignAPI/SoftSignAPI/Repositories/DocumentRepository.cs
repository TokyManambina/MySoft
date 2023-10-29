using Microsoft.EntityFrameworkCore;
using SoftSignAPI.Context;
using SoftSignAPI.Interfaces;
using SoftSignAPI.Model;

namespace SoftSignAPI.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {

        private readonly dbContext _db;

        public DocumentRepository(dbContext db)
        {
            _db = db;
        }

        public async Task<List<Document>?> GetAll(string? search = null, int? count = null, int? page = null)
        {
            try
            {
                var query = _db.Documents.AsQueryable();

                if (!string.IsNullOrEmpty(search))
                    query = query.Where(x => x.Filename.ToLower().Contains(search.ToLower()) || x.Code.ToLower().Contains(search.ToLower()));

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
        public async Task<Document?> Get(string code)
        {
            try
            {
                if (!await IsExist(code))
                    return null;

                return await _db.Documents.FirstOrDefaultAsync(x => x.Code == code);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> IsExist(string code)
        {
            return await _db.Documents.AnyAsync(x => x.Code == code);
        }
        public async Task<Document?> Create(Document document)
        {
            try
            {
                if (await IsExist(document.Code))
                    return null;

                document = _db.Documents.Add(document).Entity;

                Save();

                return document;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> Update(string code, Document updateDocument)
        {
            try
            {
                var document = await Get(code);

                if (document == null)
                    return false;

                document.Status = updateDocument.Status;

                _db.Documents.Update(document);

                return Save();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> Delete(string code)
        {
            try
            {
                var document = await Get(code);

                if (document == null)
                    return false;

                _db.Documents.Remove(document);

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
