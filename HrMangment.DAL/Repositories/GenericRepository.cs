using HrManagment.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly HrManagmentContext hrMangmentContext;
        private DbSet<T> table;

        public GenericRepository(HrManagmentContext _hrMangmentContext)
        {
            hrMangmentContext = _hrMangmentContext;
            table = hrMangmentContext.Set<T>();
        }
        public void Delete(int id)
        {
            T exsitingEntity = table.Find(id);
            if (exsitingEntity != null) table.Remove(exsitingEntity);
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            return await Task.FromResult(table);
        }

        public async Task<T> GetByIdAsync(int id)
        {

            return await table.FindAsync(id);

        }

        public async Task InsertAsync(T entity)
        {
            await table.AddAsync(entity);
        }

        public async Task SaveAsync()
        {
            await hrMangmentContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            table.Attach(entity);
            hrMangmentContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task< IEnumerable<T>> GetFilteredAsync(Func<T, bool> condition)
        {
            return  await Task.FromResult( table.Where(condition) ) ;
        }


    }
        
}
