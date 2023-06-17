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
            hrMangmentContext= _hrMangmentContext;
            table = hrMangmentContext.Set<T>();
        }
        public void Delete(int id)
        {
            T exsitingEntity = table.Find(id);
            if (exsitingEntity != null) table.Remove(exsitingEntity);
        }

        public IQueryable<T> GetAll()
        {
            return table;
        }

        public T GetById(int id)
        {
         
            return table.Find(id);
           
        }

        public void Insert(T entity)
        {
            table.Add(entity);
        }

        public void Save()
        {
            hrMangmentContext.SaveChanges();
        }

        public void Update(T entity)
        {
            table.Attach(entity);
            hrMangmentContext.Entry(entity).State = EntityState.Modified;   
        }
    }
    
}
