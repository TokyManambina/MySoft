using SoftSignAPI.Model;

namespace SoftSignAPI.Interfaces
{
    public interface IOfferRepository
    {
        Task<Offer?> Create(Offer offer);
        Task<bool> Delete(int id);
        Task<bool> Delete(string code);
        Task<Offer?> Get(int id);
        Task<Offer?> Get(string code);
        Task<List<Offer>?> GetAll(string? search =null, bool? isActive = null, int? count = null, int? page = null);
        Task<bool> IsExist(int id);
        Task<bool> IsExist(string code);
        Task<bool> Save();
        Task<bool> Update(int id, Offer updateOffer);
        Task<bool> Update(string code, Offer updateOffer);
    }
}
