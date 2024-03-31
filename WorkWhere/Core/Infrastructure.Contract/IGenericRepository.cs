using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
       
        public Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);


        public Task<T> GetAsync(int id, params Expression<Func<T, object>>[] includes);

        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        public Task<IEnumerable<T>> GetAllAsyncWithQueryBuilder(IQueryBuilder<T> queryBuilder);
        public Task<T> GetByIdWithQueryBuilder(int id, IQueryBuilder<T> queryBuilder);






    }
}
