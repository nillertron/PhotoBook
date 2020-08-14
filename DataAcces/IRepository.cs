using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAcces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task Create(T model);
        Task Delete(T model);
        Task Edit(T model);
        Task<T> Get(int id);
        Task<List<T>> GetAll();
    }
}