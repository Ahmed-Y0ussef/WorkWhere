using Core.Entities;
using Core.Infrastructure.Contract;
using Infrastructure.Dbcontext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class Unitofwork : IUnitofwork
    {
        private readonly ApplicationDbContext _dbContext;

        private Dictionary<Type, object> _repositories;
       

        public Unitofwork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
           _repositories = new Dictionary<Type, object>();
        }
        public async Task<int> Complete()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
       => await _dbContext.DisposeAsync();



        public IGenericRepo<T> GetRepo<T>() where T : BaseEntity
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return (IGenericRepo<T>)_repositories[typeof(T)];
            }

            var repository = new GenericRepo<T>(_dbContext);
            _repositories.Add(typeof(T), repository);
            return repository;
        }



    }
}
