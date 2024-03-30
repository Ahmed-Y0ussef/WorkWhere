using Core.Entities;
using Core.Infrastructure.Contract;
using Infrastructure.Dbcontext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext dbcontext;

        public GenericRepository(ApplicationDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbcontext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await dbcontext.Set<T>().FindAsync(id);
        }

        public async Task CreateAsync(T entity)
        {
            await dbcontext.Set<T>().AddAsync(entity);
            //await dbcontext.SaveChangesAsync(); // You may want to save changes after adding the entity
        }

        public async Task UpdateAsync(T entity)
        {
            dbcontext.Set<T>().Update(entity);
           //await dbcontext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            dbcontext.Set<T>().Remove(entity);
            //await dbcontext.SaveChangesAsync();
        }
    }
}
