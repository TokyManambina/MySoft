﻿using SoftSignAPI.Model;

namespace SoftSignAPI.Interfaces
{
    public interface IUserDocumentRepository
    {
        Task<UserDocument?> Create(UserDocument newUserDocument);
        Task<bool> CreateRange(List<UserDocument> newUserDocuments);

		Task<bool> Delete(int id);
        Task<UserDocument?> Get(int? id = null, Guid? userId = null, string? code = null);
        Task<List<UserDocument>?> GetAll(Guid? userId = null, string? code = null, int? count = null, int? page = null);
		Task<bool> IsExist(int id);
        bool Save();
        Task<bool> Update(int id, UserDocument updateUserDocument);
        Task<bool> Validate(string documentCode, Guid UserId);
		Task <bool> UpdateSignAndParaphe(UserDocument? userDocument, string? fields, string? signImage, string? parapheImage);
		Task <bool> UpdateSignAndParaphe(string code, Guid userId, string? signImage, string? parapheImage);
	}
}
