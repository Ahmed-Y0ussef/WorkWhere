using Core.Entities;
using Core.Infrastructure.Contract;
using Infrastructure.Dbcontext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext dbcontext;

        public GenericRepo(ApplicationDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
         => await GetIncludes(includes).ToListAsync();


        public async Task<T?> GetById(int id, params Expression<Func<T, object>>[] includes)
         => await GetIncludes(includes).FirstOrDefaultAsync(u => u.Id == id);

        public async Task<IEnumerable<T>> GetAllAsyncWithQueryBuilder(IQueryBuilder<T> queryBuilder)
        => await BuildQuery(queryBuilder).ToListAsync();
        public async Task<T> GetByIdWithQueryBuilder(int id, IQueryBuilder<T> queryBuilder)
        => await BuildQuery(queryBuilder).FirstOrDefaultAsync(u => u.Id == id);


        public async Task Add(T entity)
            => await dbcontext.Set<T>().AddAsync(entity);

        public async Task Update(T entity)
        => dbcontext.Update(entity);

        public async Task Delete(T entity)
        => dbcontext.Remove(entity);


        private IQueryable<T> GetIncludes(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbcontext.Set<T>();
            if (includes != null)
                foreach (var item in includes)
                    query = query.Include(item);

            return query;
        }


        private IQueryable<T> BuildQuery(IQueryBuilder<T> queryBuilder) //build query
        {
            IQueryable<T> query = dbcontext.Set<T>();

            if (queryBuilder.Criteria != null)
                query = query.Where(queryBuilder.Criteria);

            if (queryBuilder.Includes != null)
                foreach (var item in queryBuilder.Includes)
                    query = query.Include(item);

            if (queryBuilder.Skip != 0)
                query = query.Skip(queryBuilder.Skip);

            if (queryBuilder.Take != 0)
                query = query.Take(queryBuilder.Take);

            return query;
        }



    }
}
