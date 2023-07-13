using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.DAL.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetFilteredAsync(Func<T, bool> predicate);
        Task<IEnumerable<T>> GetFilteredIncluded(Func<T, bool> condition, string propPath);
        Task<IEnumerable<T>> GetIncluded(string propPath);
        Task<IEnumerable<T>>GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task InsertAsync(T entity);  
        void Update(T entity);  
        Task Delete(int id);
        Task SaveAsync();
        Task InsertListAsync(List<T> list);
        public T GetByIdAsNoTracking(int id);


    }
}
