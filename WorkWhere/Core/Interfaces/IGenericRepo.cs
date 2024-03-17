using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        public Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        
        public Task Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);
        public Task<T> GetById(int id, params Expression<Func<T, object>>[] includes);
       
    }
}
