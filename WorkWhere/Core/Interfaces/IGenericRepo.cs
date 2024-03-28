using Core.Entities;
using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        public Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        
        public Task Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);
        public Task<T> GetById(int id, params Expression<Func<T, object>>[] includes);
        public  Task<IEnumerable<T>> GetAllAsyncWithQueryBuilder(IQueryBuilder<T> queryBuilder);
        public Task<T> GetByIdWithQueryBuilder(int id, IQueryBuilder<T> queryBuilder );


    }
}
