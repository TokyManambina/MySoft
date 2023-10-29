using SoftSignAPI.Model;

namespace SoftSignAPI.Interfaces
{
    public interface IFieldRepository
    {
        Task<Field?> Create(Field field);
        Task<bool> Delete(int id);
        Task<Field?> Get(int id);
        Task<List<Field>?> GetAll(int? userDocumentId = null, int? count = null, int? page = null);
        Task<bool> IsExist(int id);
        bool Save();
        Task<bool> Update(int id, Field updateField);
    }
}
