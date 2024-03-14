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
    internal class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        ApplicationDbContext AppContext;
        public GenericRepo(ApplicationDbContext _AppContext)
        {
            AppContext = _AppContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
            => await AppContext.Set<T>().ToListAsync();
       
       
    }
}
