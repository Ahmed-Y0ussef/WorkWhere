using Core.Entities;
using Core.Interfaces;
using Infrastructure.Dbcontext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        ApplicationDbContext AppContext;
        public GenericRepo(ApplicationDbContext _AppContext)
        {
            AppContext = _AppContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            //if(typeof(T)==typeof(Place))
                //return await AppContext
            return   await AppContext.Set<T>().ToListAsync();
        }
        public async Task Add(T entity)
       =>await AppContext.Set<T>().AddAsync(entity);

        public void Delete(T entity)
        => AppContext.Remove(entity);

        public async Task GetById(int id)
        =>await AppContext.Set<T>().FindAsync(id);

        public void Update(T entity)
        => AppContext.Update(entity);
    }
}
