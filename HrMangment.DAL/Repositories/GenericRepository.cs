﻿using HrManagment.DAL.DBContext;
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
        public async Task Delete(int id)
        {
            T exsitingEntity = await table.FindAsync(id);
            if (exsitingEntity != null) table.Remove(exsitingEntity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await table.ToListAsync();
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

        public async Task Update(T entity)
        {
            table.Attach(entity);
            hrMangmentContext.Entry(entity).State = EntityState.Modified;

        }

        public async Task InsertListAsync(List<T> list)
        {
            await table.AddRangeAsync(list);
        }

        public async Task<IEnumerable<T>> GetFilteredAsync(Func<T, bool> condition)
        {
            return await Task.FromResult(table.Where(condition));
        }

      

        public async Task<IEnumerable<T>> GetFilteredIncluded(Func<T, bool> condition, string propPath)
        {
            var resultList = table.Include(propPath).Where(condition).ToList();
            table.Local.Clear();
            foreach (var entity in resultList)
            {
                hrMangmentContext.Entry(entity).State = EntityState.Detached;
            }
            return resultList;
        }
        public async Task<IEnumerable<T>> GetIncluded( string propPath)
        {
            var resultList = table.Include(propPath).ToList();
            table.Local.Clear();
            foreach (var entity in resultList)
            {
                hrMangmentContext.Entry(entity).State = EntityState.Detached;
            }
            return resultList;

        }


        public async Task<T> GetByIdAsynAsNoTracking(int id)
        {
            var entity = await table.FindAsync(id);
            if (entity != null)
                hrMangmentContext.Entry(entity).State = EntityState.Detached;

            return entity;
        }
    }

}
