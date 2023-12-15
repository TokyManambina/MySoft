using SoftSignAPI.Model;

namespace SoftSignAPI.Interfaces
{
    public interface IRepository<T>
    {
        //GetOne T
        Task<T?> Get(object id);
        /*
         * GetMany T
         * id : if select by parent (if needed)
         * if(using pagination or infinit scroll)
         * count : number of object that i take 
         * page : page number
        */
        
        Task<List<T>?> GetAll(object? id = null, int? count = null, int? page = null);

        //IsExist name, code ...
        Task<bool> IsExist(string name);

        //IsExist Id
        Task<bool> IsExist(object id);

        //Create T
        Task<T?> Create(T newObject);

        //Update T if Id = ???
        Task<bool> Update(object id, T updateObject);

        //Delete T 
        Task<bool> Delete(object id);

        //Save All
        Task<bool> Save();
    }
}
