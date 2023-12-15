using Microsoft.EntityFrameworkCore;
using SoftSignAPI.Context;
using SoftSignAPI.Dto;
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

        public async Task<List<Document>> GetAll(string? search = null, int? count = null, int? page = null)
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

		public async Task<List<ShowDocument>?> GetReceivedDocument(User user, string? search = null, int? count = null, int? page = null)
		{
			try
			{
				var query = _db.Documents
					.Include(x => x.UserDocuments)
					.ThenInclude(x => x.User)
					.Where(x => x.UserDocuments.Any(y => y.UserId == user.Id && (y.MyTurn || y.IsFinished) && y.Step != 0))
					.OrderByDescending(x => x.DateSend)
					.Select(x => new ShowDocument
					{
						Code = x.Code,
						DateSend = x.DateSend,
						Message = x.Message,
						Object = x.Object,
						Title = x.Title,
						Status = x.Status,
						De = x.UserDocuments.Where(y => y.Step == 0).Select(y => y.User.Email).FirstOrDefault(),
						Pour = CompareMail(user.Email, x.UserDocuments.OrderByDescending(y => y.Step).Select(y => y.User.Email).FirstOrDefault())
					})
					.AsQueryable();


				if (!string.IsNullOrEmpty(search))
					query = query.Where(x => x.Object.ToLower().Contains(search.ToLower()) || x.Message.ToLower().Contains(search.ToLower()));

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
		public async Task<List<ShowDocument>?> GetSendedDocument(User user, string? search = null, int? count = null, int? page = null)
		{
			try
			{
                var query = _db.Documents
                    .Include(x => x.UserDocuments)
                    .ThenInclude(x => x.User)
                    .Where(x => x.UserDocuments.Count > 1 && x.UserDocuments.Any(y => y.UserId == user.Id && y.Step == 0))
                    .OrderByDescending(x => x.DateSend)
                    .Select(x=>new ShowDocument
                    {
                        Code = x.Code,
                        DateSend = x.DateSend,
                        Message = x.Message,
                        Object = x.Object,
                        Title = x.Title,
                        Status = x.Status,
                        De = "",
                        Pour = x.UserDocuments.OrderByDescending(y=>y.Step).Select(y=>y.User.Email).FirstOrDefault()
                    })
                    .AsQueryable();
                 

				if (!string.IsNullOrEmpty(search))
					query = query.Where(x => x.Object.ToLower().Contains(search.ToLower()) || x.Message.ToLower().Contains(search.ToLower()));

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
		public async Task<List<ShowDocument>?> GetOwnerDocument(User user, string? search = null, int? count = null, int? page = null)
		{
			try
			{
				var query = _db.Documents
					.Include(x => x.UserDocuments)
					.ThenInclude(x => x.User)
					.Where(x => x.UserDocuments.Count == 1 && x.UserDocuments.Any(y => y.UserId == user.Id && y.Step == 0))
					.OrderByDescending(x => x.DateSend)
					.Select(x => new ShowDocument
					{
						Code = x.Code,
						DateSend = x.DateSend,
						Message = x.Message,
						Object = x.Object,
						Title = x.Title,
						Status = x.Status,
						De = "",
						Pour = ""
					})
					.AsQueryable();

				if (!string.IsNullOrEmpty(search))
					query = query.Where(x => 
                        x.Object.ToLower().Contains(search.ToLower()) || 
                        x.Message.ToLower().Contains(search.ToLower()) || 
                        x.Title.ToLower().Contains(search.ToLower()) 
                    );  

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
		public async Task<List<ShowDocument>?> GetDocuments(User user, DocumentStat? stat = null, string? search = null, int? count = null, int? page = null)
		{
			try
			{
				var query = _db.Documents
					.Include(x => x.UserDocuments)
					.ThenInclude(x => x.User)
					.OrderByDescending(x => x.DateSend)
					.Select(x => new ShowDocument
					{
						Code = x.Code,
						DateSend = x.DateSend,
						Message = x.Message,
						Object = x.Object,
						Title = x.Title,
						Status = x.Status,
						De = CompareMail(user.Email, x.UserDocuments.Where(y => y.Step == 0).Select(y => y.User.Email).FirstOrDefault()),
						Pour = CompareMail(user.Email, x.UserDocuments.OrderByDescending(y => y.Step).Select(y => y.User.Email).FirstOrDefault())
					})
					.AsQueryable();


				if (stat != null)
					query = query.Where(x => x.Status == stat);

				if (!string.IsNullOrEmpty(search))
					query = query.Where(x => 
                        x.Object.ToLower().Contains(search.ToLower()) || 
                        x.Message.ToLower().Contains(search.ToLower()) || 
                        x.Title.ToLower().Contains(search.ToLower()) 
                    );  

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
		private static string? CompareMail(string a, string b)
        {
            return a == b ? "" : b; 
        }

		public async Task<List<DocInfo>?> GetDocumentInfo(Guid userId)
		{
			try
			{

				return await _db.UserDocuments
					.Include(x => x.User)
					.Include(x => x.Document)
					.Where(x => x.UserId == userId && (x.IsFinished || x.MyTurn || x.Step == 0))
					.Select(x => x.Document.Status)
					.GroupBy(x => x)
					.Select(x => new DocInfo
					{
                        Stat = x.Key,
                        Count = x.Count()
					})
					.ToListAsync(); ;
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

                return await _db.Documents.Include(x=>x.UserDocuments).ThenInclude(x=>x.Fields).FirstOrDefaultAsync(x => x.Code == code);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
		public async Task<DocumentViewAction?> GetWithUser(string code, Guid userId)
		{
			try
			{
				if (!await IsExist(code))
					return null;

				var userDocument = await _db.UserDocuments
					.Include(x => x.Document)
					.Include(x=>x.Fields)
					.FirstOrDefaultAsync(x => x.DocumentCode == code && x.UserId == userId);


				return new DocumentViewAction
				{
					Code = code,
					DateSend = userDocument.Document.DateSend,
					Filename = userDocument.Document.Filename,
					Message = userDocument.Document.Message,
					Object = userDocument.Document.Object,
					Title = userDocument.Document.Title,
					Status = userDocument.Document.Status,
					MyTurn = userDocument.MyTurn,
					hasSign = userDocument.Fields.Any(x=>x.FieldType == FieldType.Signature),
					hasParaphe = userDocument.Fields.Any(x=>x.FieldType == FieldType.Paraphe),
				};
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
