using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.DAL.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IQueryable<T>>GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task InsertAsync(T entity);  
        void Update(T entity);  
        void Delete(int id);
        Task SaveAsync();



        
    }
}
