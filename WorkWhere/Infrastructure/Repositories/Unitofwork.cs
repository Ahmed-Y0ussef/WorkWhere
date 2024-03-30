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
    public class Unitofwork : IUnitofwork
    {
        private readonly ApplicationDbContext _dbContext;
        private Dictionary<Type, object> _repositories;
        private UserRepository _userRepository;


        public Unitofwork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Dictionary<Type, object>();
        }

        public UserRepository UserRepository => _userRepository ??= new UserRepository(_dbContext);

        public IGenericRepository<T> GetRepository<T>() where T : BaseEntity
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return (IGenericRepository<T>)_repositories[typeof(T)];
            }

            var repository = new GenericRepository<T>(_dbContext);
            _repositories.Add(typeof(T), repository);
            return repository;
        }

        //public T GetRepositoryTwo<T>() where T : BaseEntity
        //{
        //    if (_repositories.TryGetValue(typeof(T), out var repository))
        //    {
        //        return (T)repository;
        //    }

        //    var newRepository = new GenericRepository<T>(_dbContext);
        //    _repositories.Add(typeof(T), newRepository);
        //    return newRepository;
        //}

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
       => await _dbContext.DisposeAsync();

        

    }
}
