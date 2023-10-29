using SoftSignAPI.Model;

namespace SoftSignAPI.Interfaces
{
    public interface ISocietyRepository
    {
        Task<Society?> Create(Society society);
        Task<bool> Delete(Guid id);
        Task<Society?> Get(Guid id);
        Task<List<Society>?> GetAll(string? search = null, int? count = null, int? page = null);
        Task<Society?> GetByUser(string mail);
        Task<bool> IsExist(Guid id);
        Task<bool> Save();
        Task<bool> Update(Guid id, Society updateSociety);
    }
}
