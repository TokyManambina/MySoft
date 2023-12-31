﻿using Microsoft.EntityFrameworkCore;
using SoftSignAPI.Context;
using SoftSignAPI.Interfaces;
using SoftSignAPI.Model;
using System.Reflection.Metadata;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;
using PdfSharp.Drawing;
using Newtonsoft.Json;
using SoftSignAPI.Dto;

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
                var query = _db.UserDocuments.Include(x => x.Document).Include(x => x.Fields).AsQueryable();

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
                var query = _db.UserDocuments.Include(x=>x.User).Include(x=>x.Document).AsQueryable();

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
                Save();

                return newUserDocument;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
		public async Task<bool> CreateRange(List<UserDocument> newUserDocuments)
		{
			try
			{
                await _db.UserDocuments.AddRangeAsync(newUserDocuments);

                await _db.SaveChangesAsync();
                return true;
			}
			catch (Exception ex)
			{
                return false;
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

                userDocument.UserId = updateUserDocument.UserId;
                userDocument.Step = updateUserDocument.Step;
                userDocument.Role = updateUserDocument.Role;
                userDocument.IsFinished = updateUserDocument.IsFinished;

                _db.UserDocuments.Update(userDocument);

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
                var userDocument = await Get(id);

                if (userDocument == null)
                    return false;

                _db.UserDocuments.Remove(userDocument);

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

		public async Task<bool> UpdateSignAndParaphe(UserDocument? userDocument, string? fields, string? signImage, string? parapheImage)
		{
            try
            {
				userDocument.Fields = JsonConvert.DeserializeObject<List<Field>>(fields);
				userDocument.Signature = datatoimage(signImage);
                userDocument.Paraphe = datatoimage(parapheImage);
                _db.UserDocuments.Update(userDocument);
                await _db.SaveChangesAsync();
                return true;
            }catch (Exception ex)
            {
				throw new Exception(ex.Message);
				return false;
			}
		}
		public async Task<bool> UpdateSignAndParaphe(string code, Guid userId, string? signImage, string? parapheImage)
		{
			try
			{
                var userDocument = await _db.UserDocuments.Where(x => x.UserId == userId && x.DocumentCode == code).FirstOrDefaultAsync();

				userDocument.Signature = datatoimage(signImage);
				userDocument.Paraphe = datatoimage(parapheImage);

				_db.UserDocuments.Update(userDocument);
				await _db.SaveChangesAsync();

                return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
				return false;
			}
		}

		public async Task<bool> Validate(string code, Guid userId)
		{
			try
			{
				var udoc = await _db.UserDocuments.Include(x => x.Document).Where(x => x.UserId == userId && x.DocumentCode == code).FirstOrDefaultAsync();

				if (udoc == null)
					return false;

				udoc.MyTurn = false;
				udoc.IsFinished = true;
				_db.UserDocuments.Update(udoc);
				var nextUser = await _db.UserDocuments.Where(x => x.Step == udoc.Step + 1 && x.DocumentCode == code).FirstOrDefaultAsync();

				if (nextUser == null)
				{
					udoc.Document.Status = DocumentStat.Completed;
					_db.UserDocuments.Update(udoc);
				}
				else
				{
					nextUser.MyTurn = true;
					_db.UserDocuments.Update(nextUser);
				}

				

				await _db.SaveChangesAsync();
				return true;

			}
			catch (Exception ex)
			{
				return false;
				throw new Exception(ex.Message);
			}
		}
		byte[] datatoimage(string data)
		{
			var matchGroups = Regex.Match(data, @"^data:((?<type>[\w\/]+))?;base64,(?<data>.+)$").Groups;
			var base64Data = matchGroups["data"].Value;
			var binData = Convert.FromBase64String(base64Data);
			return binData;
		}
	}
}
